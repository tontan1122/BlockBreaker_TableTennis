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
    private CursorController cursorController;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField, Header("クラス参照：音源関係")]
    private GameAudioManager audioManager;
    [SerializeField, Header("クラス参照：データ関係")]
    private ClearStageData clearStageData;

    [SerializeField, Header("現在のレベル")]
    private int currentLevel = 0;

    private int currentMaxLevel = 0;    // 現在の最高レベル
    private int clickCount = 0;
    private int heightUnavailableClick = Screen.height / 5 * 4;

    private bool isFirstPlay = true;    // ゲームを起動して最初のプレイかどうか
    private bool isHintPanelActive = false; // ヒントパネルを一度表示したかどうか
    private bool isStopGame = false;    // ボールがプレイヤーの情報を使うかどうか

    private void Start()
    {
        ballManager.SetHeightClick = heightUnavailableClick;

        // ポーズ中は弾を打てないようにする
        PauseUIController.OnPaused.Subscribe(_ =>
        {
            ballManager.SetIsShot = false;
        })
            .AddTo(this);
    }

    private void Update()
    {
        QuitTheGame.GetInstance.EndGame();

        GameState();
    }

    private void GameState()
    {
        switch (scene)
        {
            case Scene.TITLE_INIT:
                uiManager.TitleUI();
                clickCount = 0;
                SetState(Scene.TITLE);
                break;

            case Scene.TITLE:
                if (!ballManager.isMove)    //もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      //ボールを離す初期位置を設定
                }
                if (!isStopGame)    //もしボールが動いていないなら
                {
                    ballManager.SetIsShot = playerController.GetIsControl;            //ボールを放てるようにする
                }
                if (Input.GetMouseButtonDown(0) && !uiManager.getSettingActive && Input.mousePosition.y <= heightUnavailableClick)  // 設定画面が出ているか＆クリックした箇所がしていの高さ以上なら
                {
                    clickCount++;
                }
                if (clickCount >= 2)
                {
                    audioManager.ClickButtonAudio();
                    SetState(Scene.TITLE_END);
                }
                break;

            case Scene.TITLE_END:
                SetState(Scene.STAGESELECT_INIT);
                break;

            case Scene.STAGESELECT_INIT:
                currentMaxLevel = clearStageData.LoadClearStage();  //前回のステージ履歴にならないようにデータをロード
                if (isFirstPlay)    //最初のプレイなら
                {
                    uiManager.StageSelectUI(true, currentMaxLevel, currentMaxLevel);    // 現在の最高到達ステージにバーを移動
                }
                else
                {
                    uiManager.StageSelectUI(true, currentMaxLevel, currentLevel);   // 以前にプレイしたステージにバーを移動
                }
                SetState(Scene.STAGESELECT);
                break;

            case Scene.STAGESELECT:
                ballManager.SetIsShot = playerController.GetIsControl;      //ボールを放てるようにする
                //UIのボタンを押すまでこのシーン
                break;

            case Scene.STAGESELECT_END:
                uiManager.StageSelectUI(false, currentLevel, currentLevel);
                isFirstPlay = false;
                SetState(Scene.GAME_INIT);
                break;

            case Scene.GAME_INIT:
                uiManager.GameUI(true);


                // パネルがActiveになったフレームだとテキスト変更ができないため1フレ待機
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    // 1フレーム後にここの処理が実行される
                    uiManager.GameUI_ChangeStageText(currentLevel);     //ステージ数表記の更新
                    ballManager.MissCount = 0;
                    uiManager.GameUI_MissCountText(0);  //ミスカウントテキストのリセット
                    isHintPanelActive = false;  // ヒントパネルのリセット
                }));


                //カメラの移動
                cameraController.MoveNextStageCamera(stageManager.ContinuousClear);

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageInit(currentLevel);     //ステージ生成

                cursorController.CursorOff();

                SetState(Scene.GAME);
                break;

            case Scene.GAME:
                if (!PauseUIController.IsPaused && !isStopGame)  // ポーズ中でなければ
                {
                    ballManager.SetIsShot = playerController.GetIsControl;//playerの状態でボールを発射できるかどうか決める
                }

                if (!ballManager.isMove)    //もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.R))    //リスタート
                {
                    stageManager.StageReset();
                    ballManager.BallRestart();
                    ballManager.MissCount++;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);  //ミスカウントテキストの更新
                }

                if (stageManager.IsClear)
                {
                    audioManager.ClearStageAudio();
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) //もしミスったら
                {
                    ballManager.IsMiss = false;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);   //ミスカウントの表示
                    stageManager.StageReset(); //ブロックを配置し直し
                }
                if (ballManager.MissCount == 10 && !isHintPanelActive)
                {
                    HintEvent();  // ミス10回目のイベント
                    isHintPanelActive = true;
                }
                break;

            case Scene.GAME_END:
                stageManager.ClearStage();    //死なないように床の配置
                cursorController.CursorOn();

                //クリアしたステージの保存
                clearStageData.SaveClearStage(currentLevel);

                SetState(Scene.RESULT_INIT);

                break;

            case Scene.RESULT_INIT:
                uiManager.ResultUI(true);

                // パネルがActiveになったフレームだとテキスト変更ができないため1フレ待機
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    uiManager.ResultUI_CheckStage(currentLevel);
                }));

                SetState(Scene.RESULT);
                break;

            case Scene.RESULT:
                if (Input.GetKeyDown(KeyCode.R))    // リスタート
                {
                    stageManager.StageReset(); //ステージは変えずに生成
                    ballManager.BallRestart();

                    uiManager.ResultUI(false);

                    stageManager.IsClear = false;   //クリア条件をリセット

                    ballManager.MissCount = 0;
                    uiManager.GameUI_MissCountText(0);

                    stageManager.ClearStageReset();             //床、または天井などの削除

                    SetState(Scene.GAME);
                }

                if (!ballManager.isMove)    //もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                    ballManager.SetIsShot = playerController.GetIsControl;      //ボールを放てるようにする
                }
                //ボタンが押されるまで
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
        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();
        SetState(Scene.STAGESELECT_END);
    }

    /// <summary>
    /// リザルトのボタンから呼び出される
    /// </summary>
    public void NextStage()
    {
        uiManager.ResultUI(false);

        currentLevel++;

        //SE
        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();

        stageManager.StageClearReset();

        SetState(Scene.GAME_INIT);
    }

    /// <summary>
    /// リザルトのボタンから呼び出される
    /// </summary>
    public void MoveTitle()
    {
        uiManager.ResultUI(false);
        uiManager.GameUI(false);

        //SE
        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();

        //カメラをタイトルの場所まで戻す
        cameraController.MoveTitleCamera(stageManager.Reset);

        playerController.TitlePosMove();
        ballManager.BallReset();
        ballManager.BackTitle();

        SetState(Scene.TITLE_INIT);
    }

    public void RestartStage()
    {
        stageManager.StageReset();
        ballManager.BallRestart();
        ballManager.MissCount++;
        uiManager.GameUI_MissCountText(ballManager.MissCount);  //ミスカウントテキストの更新
    }

    /// <summary>
    /// リザルトでのステージリスタート
    /// </summary>
    public void ResultRestartStage()
    {
        BackGame();
        stageManager.StageReset(); //ステージは変えずに生成
        ballManager.BallRestart();

        uiManager.ResultUI(false);

        stageManager.IsClear = false;   //クリア条件をリセット

        ballManager.MissCount = 0;
        uiManager.GameUI_MissCountText(0);

        stageManager.ClearStageReset();             //床、または天井などの削除


        SetState(Scene.GAME);
    }

    /// <summary>
    /// ミス10回でヒントパネルを出す
    /// ヒントパネルはリザルトからリスタートした場合は出さないようにする
    /// </summary>
    private void HintEvent()
    {
        // ヒントを勧めるパネルを表示
        uiManager.GameUI_HintPanel(true);
        cursorController.CursorOn();
        isStopGame = true;  // ゲームを一時停止する
        ballManager.SetIsShot = false;  // ボールを打てなくする
    }

    public void BackGame()
    {
        // ゲームに戻るときにボールを放たないようにするための処理
        ballManager.SetIsShot = false;
        isStopGame = true;  // 弾を発射しないようにするため

        // カーソルを非表示に戻す
        cursorController.CursorOff();

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
        cursorController.CursorOn();
        isStopGame = true;  // ゲームを一時停止する
        ballManager.SetIsShot = false;  // ボールを放てないようにする
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
