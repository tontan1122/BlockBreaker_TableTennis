using System;
using System.Collections;
using UnityEngine;

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


    private int clickCount = 0;

    [SerializeField, Header("現在のレベル")]
    private int currentLevel = 0;

    void Update()
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
                    ballManager.SetIsShot = playerController.GetIsControl;            //ボールを放てるようにする
                }
                if (Input.GetMouseButtonDown(0))
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
                currentLevel = clearStageData.LoadClearStage();  //前回のステージ履歴にならないようにデータをロード
                uiManager.StageSelectUI(true, currentLevel);
                SetState(Scene.STAGESELECT);
                break;

            case Scene.STAGESELECT:
                ballManager.SetIsShot = playerController.GetIsControl;      //ボールを放てるようにする
                //UIのボタンを押すまでこのシーン
                break;

            case Scene.STAGESELECT_END:
                uiManager.StageSelectUI(false, currentLevel);
                SetState(Scene.GAME_INIT);
                break;

            case Scene.GAME_INIT:
                uiManager.GameUI(true);

                // パネルがActiveになったフレームだとテキスト変更ができないため1フレ待機
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    // 1フレーム後にここの処理が実行される
                    uiManager.GameUI_ChangeStageText(currentLevel);     //ステージ数表記の更新
                    uiManager.GameUI_MissCountText(0);  //ミスカウントテキストのリセット
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
                ballManager.SetIsShot = playerController.GetIsControl;//playerの状態でボールを発射できるかどうか決める

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
                if (Input.GetKeyDown(KeyCode.H))    // ヒントスタート
                {
                    stageManager.HintClick();
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

    public void BackGame()
    {
        ballManager.BallReset();
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
