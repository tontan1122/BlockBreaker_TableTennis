using System;
using System.Collections;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲームの状態遷移
/// </summary>
public enum Scene
{
    TITLE_INIT,
    TITLE,
    TITLE_END,
    STAGESELECT_INIT,
    STAGESELECT,
    STAGESELECT_END,
    GAME_INIT,
    GAME,
    GAME_END,
    RESULT_INIT,
    RESULT,
}

public static class GlobalConst
{
    public const int STAGE_SIZE_Y = 15;
    public const int TITLE_POSITION = -18;
    public const float BALL_SIZE = 0.5f;
    public static int heightUnavailableClick; // 画面クリックができない範囲
}

public static class StaticVariable
{
    public static bool isPosibleClickOperation = true;   // 画面クリックができるかどうか
}

/// <summary>
/// ゲームの管理クラス
/// </summary>
public class GameManager : MonoBehaviour
{
    public Scene scene;

    [SerializeField, Header("クラス参照:UI関係")]
    private UIManager uiManager;
    [SerializeField, Header("クラス参照:挙動関係")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField, Header("クラス参照：音源関係")]
    private GameAudioManager audioManager;
    [SerializeField, Header("クラス参照：データ関係")]
    private ClearStageData clearStageData;

    [SerializeField, Header("現在のレベル")]
    private int currentLevel = 0;

    private int currentMaxLevel = 0;    // 現在の最高レベル
    private int clickCount = 0;         // クリック回数
    private static readonly int TIME_NOT_CLICKED = 1800;
    private int titleTime;

    private bool isFirstPlay = true;    // ゲームを起動して最初のプレイかどうか
    private bool isHintPanelActive = false; // ヒントパネルを一度表示したかどうか
    private bool isStopGame = false;    // ボールがプレイヤーの情報を使うかどうか

    private void Start()
    {
        SetClickArea(); // クリックできる範囲を指定
        titleTime = TIME_NOT_CLICKED;

        // ポーズ中は弾を打てないようにする
        PauseUIController.OnPaused.Subscribe(_ =>
        {
            ballManager.SetIsShot = false;
        })
            .AddTo(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !uiManager.GetAnyPanelActive())
        {
            uiManager.QuitGamePanelActive(true);
            audioManager.PlayGameSE(0); // ボタンクリック音を鳴らす
        }

        GameState();
    }

    private void GameState()
    {
        switch (scene)
        {
            case Scene.TITLE_INIT:
                uiManager.OperateTitleUI();
                clickCount = 0;
                SetState(Scene.TITLE);
                break;

            case Scene.TITLE:
                if (!ballManager.GetIsMove)    // もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      // ボールを離す初期位置を設定
                }
                if (!isStopGame && !uiManager.GetAnyPanelActive())    // もしボールが動いていないなら
                {
                    ballManager.SetIsShot = playerController.GetIsControl;            // ボールを放てるようにする
                }
                if (uiManager.GetAnyPanelActive())
                {
                    ballManager.SetIsShot = false;
                }
                if (titleTime >= 0)
                {
                    titleTime--;
                }
                if (titleTime == 0 && clickCount == 0)
                {
                    titleTime = -1;
                    clickCount++;
                    ballManager.StartMove();
                    uiManager.ChangeTitleText();
                }
                if (Input.GetMouseButtonDown(0) &&
                    !uiManager.GetAnyPanelActive() &&
                    Input.mousePosition.y <= GlobalConst.heightUnavailableClick)  // 設定画面が出ているか＆クリックした箇所がしていの高さ以上なら
                {
                    clickCount++;
                }
                if (clickCount >= 2)
                {
                    audioManager.PlayGameSE(0);
                    SetState(Scene.TITLE_END);
                }
                break;

            case Scene.TITLE_END:
                SetClickArea(); // クリックできる範囲を指定
                titleTime = TIME_NOT_CLICKED;
                SetState(Scene.STAGESELECT_INIT);
                break;

            case Scene.STAGESELECT_INIT:
                currentMaxLevel = clearStageData.LoadClearStage();  // 前回のステージ履歴にならないようにデータをロード
                if (isFirstPlay)    // 最初のプレイなら
                {
                    uiManager.OperateStageSelectUI(true, currentMaxLevel, currentMaxLevel);    // 現在の最高到達ステージにバーを移動
                }
                else
                {
                    uiManager.OperateStageSelectUI(true, currentMaxLevel, currentLevel);   // 以前にプレイしたステージにバーを移動
                }
                SetState(Scene.STAGESELECT);
                break;

            case Scene.STAGESELECT:

                ballManager.SetIsShot = playerController.GetIsControl;      // ボールを放てるようにする
                // UIのボタンを押すまでこのシーン
                break;

            case Scene.STAGESELECT_END:
                uiManager.OperateStageSelectUI(false, currentLevel, currentLevel);
                isFirstPlay = false;
                SetState(Scene.GAME_INIT);
                break;

            case Scene.GAME_INIT:
                uiManager.OperateGameUI(true);
                GlobalConst.heightUnavailableClick = Screen.height / 5 * 4; // 画面クリックができない範囲を指定

                // パネルがActiveになったフレームだとテキスト変更ができないため1フレ待機
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    // 1フレーム後にここの処理が実行される
                    uiManager.OperateStageLevelText(currentLevel);     // ステージ数表記の更新
                    ballManager.MissCount = 0;
                    uiManager.OperateMissCountText(0);  // ミスカウントテキストのリセット
                    isHintPanelActive = false;  // ヒントパネルのリセット
                }));


                //カメラの移動
                cameraController.MoveCameraNextStage(stageManager.GetContinuousClear);

                playerController.MoveNextStage();
                ballManager.ResetTheBall();
                stageManager.PrepareStage(currentLevel);     // ステージ生成

                SetState(Scene.GAME);
                break;

            case Scene.GAME:
                if (!PauseUIController.IsPaused && !isStopGame && !uiManager.GetAnyPanelActive())  // ポーズ中でなければ
                {
                    ballManager.SetIsShot = playerController.GetIsControl;// playerの状態でボールを発射できるかどうか決める
                }
                if (uiManager.GetAnyPanelActive())
                {
                    ballManager.SetIsShot = false;
                }

                if (!ballManager.GetIsMove)    // もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.R))    // リスタート
                {
                    stageManager.ResetBlockLevel();
                    ballManager.RestartTheBall();
                    ballManager.MissCount++;
                    uiManager.OperateMissCountText(ballManager.MissCount);  // ミスカウントテキストの更新
                }

                if (stageManager.IsClear)
                {
                    audioManager.PlayGameSE(2);
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) // もしミスったら
                {
                    ballManager.IsMiss = false;
                    uiManager.OperateMissCountText(ballManager.MissCount);   // ミスカウントの表示
                    ballManager.SetStartPos(playerController.GetPlayerPosition);    // ボールの生成位置設定
                    stageManager.ResetBlockLevel(); // ブロックを配置し直し
                }
                if (ballManager.MissCount == 10 && !isHintPanelActive)
                {
                    HintEvent();  // ミス10回目のイベント
                    isHintPanelActive = true;
                }
                break;

            case Scene.GAME_END:
                stageManager.GenerateClearStage();    // 死なないように床の配置

                // クリアしたステージの保存
                clearStageData.SaveClearStage(currentLevel);

                SetState(Scene.RESULT_INIT);

                break;

            case Scene.RESULT_INIT:
                uiManager.SwitchResultPanelVisibility(true);

                // パネルがActiveになったフレームだとテキスト変更ができないため1フレ待機
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    uiManager.CheckStageLevel(currentLevel);
                }));

                SetState(Scene.RESULT);
                break;

            case Scene.RESULT:
                if (Input.GetKeyDown(KeyCode.R))    // リスタート
                {
                    stageManager.ResetBlockLevel(); // ステージは変えずに生成
                    ballManager.RestartTheBall();

                    uiManager.SwitchResultPanelVisibility(false);

                    stageManager.IsClear = false;   // クリア条件をリセット

                    ballManager.MissCount = 0;
                    uiManager.OperateMissCountText(0);

                    stageManager.DestroyClearStage();             // 床、または天井などの削除

                    SetState(Scene.GAME);
                }

                if (!ballManager.GetIsMove)    // もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                    ballManager.SetIsShot = playerController.GetIsControl;      // ボールを放てるようにする
                }
                // ボタンが押されるまで
                break;
        }
    }

    private void SetState(Scene state)
    {
        scene = state;
    }

    /// <summary>
    /// UIのボタンで呼び出す関数
    /// レベル選択ボタン
    /// </summary>
    /// <param name="level">選んだレベル</param>
    public void MoveGame(int level)
    {
        currentLevel = level;
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);
        SetState(Scene.STAGESELECT_END);
    }

    /// <summary>
    /// リザルトのボタンから呼び出される
    /// </summary>
    public void NextStage()
    {
        uiManager.SwitchResultPanelVisibility(false);

        currentLevel++;

        // SE
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);

        stageManager.StageClearReset();

        SetState(Scene.GAME_INIT);
    }

    /// <summary>
    /// リザルトのボタンから呼び出される
    /// </summary>
    public void MoveTitle()
    {
        uiManager.SwitchResultPanelVisibility(false);
        uiManager.OperateGameUI(false);

        // SE
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);

        // カメラをタイトルの場所まで戻す
        cameraController.MoveCameraTitle(stageManager.ResetStage);

        playerController.MoveTitlePos();
        ballManager.ResetTheBall();
        ballManager.BackTitle();

        SetState(Scene.TITLE_INIT);
    }

    /// <summary>
    /// リスタートした際の処理
    /// </summary>
    public void RestartStage()
    {
        stageManager.ResetBlockLevel();
        ballManager.RestartTheBall();
        ballManager.MissCount++;
        uiManager.OperateMissCountText(ballManager.MissCount);  //ミスカウントテキストの更新
    }

    /// <summary>
    /// リザルトでのステージリスタート
    /// </summary>
    public void ResultRestartStage()
    {
        BackGame();
        stageManager.ResetBlockLevel(); // ステージは変えずに生成
        ballManager.RestartTheBall();

        uiManager.SwitchResultPanelVisibility(false);

        stageManager.IsClear = false;   // クリア条件をリセット

        ballManager.MissCount = 0;
        uiManager.OperateMissCountText(0);

        stageManager.DestroyClearStage();             // 床、または天井などの削除


        SetState(Scene.GAME);
    }

    /// <summary>
    /// ミス10回でヒントパネルを出す
    /// ヒントパネルはリザルトからリスタートした場合は出さないようにする
    /// </summary>
    private void HintEvent()
    {
        // ヒントを勧めるパネルを表示
        uiManager.SwitchHintPanelVisibility(true);
        isStopGame = true;  // ゲームを一時停止する
        ballManager.SetIsShot = false;  // ボールを打てなくする
    }

    public void BackGame()
    {
        // ゲームに戻るときにボールを放たないようにするための処理
        ballManager.SetIsShot = false;
        isStopGame = true;  // 弾を発射しないようにするため

        // 60F待ってからボールを放てるようにする
        StartCoroutine(DelayFrame(60.0f, () =>
        {
            ballManager.SetIsShot = true;
            isStopGame = false; // ゲームを再生する
        }));
    }

    /// <summary>
    /// 設定画面を開く
    /// </summary>
    public void SettingPanelActive()
    {
        uiManager.SettingActive(true);
        isStopGame = true;  // ゲームを一時停止する
        ballManager.SetIsShot = false;  // ボールを放てないようにする
    }

    /// <summary>
    /// クリックできる範囲を指定
    /// </summary>
    private void SetClickArea()
    {
        GlobalConst.heightUnavailableClick = Screen.height / 6 * 5; // 画面クリックができない範囲を指定
    }

    /// <summary>
    /// 渡されたフレームを待機する
    /// </summary>
    private IEnumerator DelayFrame(float frames, Action action)
    {
        yield return frames;
        action?.Invoke();
    }
}
