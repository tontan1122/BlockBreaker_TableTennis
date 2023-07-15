using DG.Tweening;

using UnityEngine;

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

public class GameManager : MonoBehaviour
{
    public Scene scene;

    [SerializeField, Header("ステージセレクトパネル")]
    private GameObject stageSelectPanel;

    [SerializeField, Header("ゲームパネル")]
    private GameObject gamePanel;

    [SerializeField, Header("リザルトパネル")]
    private GameObject resultPanel;

    [SerializeField, Header("カメラのオブジェクト")]
    private GameObject cameraObject;

    [SerializeField, Header("クラス参照:UI関係")]
    private SelectStageController selectStageController;
    [SerializeField]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;
    [SerializeField, Header("クラス参照:挙動関係")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private CursorController cursorController;
    [SerializeField, Header("クラス参照：音源関係")]
    private GameAudioManager audioManager;


    private int clickCount = 0;

    [SerializeField, Header("現在のレベル")]
    private int currentLevel = 0;

    void Start()
    {
        stageSelectPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);

    }

    void Update()
    {
        switch (scene)
        {
            case Scene.TITLE_INIT:
                smoothBlinkingText.TextDisplay();       //テキストの浮遊感の表現
                clickCount = 0;
                SetState(Scene.TITLE);
                break;
            case Scene.TITLE:
                if (!ballManager.isMove)    //もしボールが動いていないなら
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      //ボールを離す初期位置を設定
                    ballManager.SetIsShot = playerController.GetIsControl;            //ボールを放てるようにする
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    audioManager.ClickButtonAudio();
                    SetState(Scene.TITLE_END);
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
                stageSelectPanel.SetActive(true);
                currentLevel = selectStageController.LoadClearStage();  //前回のステージ履歴にならないようにデータをロード
                selectStageController.CheakSelectPush(currentLevel);    //ボタンのオンオフ更新
                SetState(Scene.STAGESELECT);
                break;
            case Scene.STAGESELECT:
                ballManager.SetIsShot = playerController.GetIsControl;      //ボールを放てるようにする
                //ボタンを押すまでこのシーン
                break;
            case Scene.STAGESELECT_END:
                stageSelectPanel.SetActive(false);
                SetState(Scene.GAME_INIT);
                break;
            case Scene.GAME_INIT:
                gamePanel.SetActive(true);
                //カメラの移動
                cameraObject.transform.DOMove(new Vector3(0, stageManager.ContinuousClear * 15, -10.0f), 1.0f)
                    .SetEase(Ease.InOutCubic);

                gameUIController.ChangeStageText(currentLevel);     //ステージ数表記の更新
                gameUIController.MissCountText(0);  //ミスカウントテキストのリセット

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageGeneration(currentLevel);     //ステージ生成

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
                    stageManager.ResetBlocks();
                    ballManager.BallReset();
                    ballManager.MissCount++;
                    gameUIController.MissCountText(ballManager.MissCount);  //ミスカウントテキストの更新
                }

                if (stageManager.IsClear)
                {
                    audioManager.ClearStageAudio();
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) //もしミスったら
                {
                    ballManager.IsMiss = false;
                    gameUIController.MissCountText(ballManager.MissCount);   //ミスカウントの表示
                    stageManager.ResetBlocks(); //ブロックを配置し直し
                }
                break;
            case Scene.GAME_END:
                stageManager.ClearBar();    //死なないように床の配置
                cursorController.CursorOn();

                //クリアしたステージの保存
                selectStageController.SaveClearStage(currentLevel);

                SetState(Scene.RESULT_INIT);

                break;
            case Scene.RESULT_INIT:
                resultPanel.SetActive(true);
                resultController.CheckFinalStage(currentLevel);
                SetState(Scene.RESULT);
                break;
            case Scene.RESULT:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    stageManager.ResetBlocks(); //ステージは変えずに生成
                    ballManager.BallReset();
                    resultPanel.SetActive(false);

                    stageManager.IsClear = false;   //クリア条件をリセット

                    ballManager.MissCount = 0;
                    gameUIController.MissCountText(0);  //ミスカウントテキストのリセット

                    Destroy(stageManager.GetCloneFloor);        //床の削除

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

    public void NextStage()
    {
        resultPanel.SetActive(false);

        currentLevel++;

        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();

        stageManager.StageMove();
        ballManager.BallReset();

        SetState(Scene.GAME_INIT);
    }

    public void MoveTitle()
    {
        resultPanel.SetActive(false);
        gamePanel.SetActive(false);

        //カメラをタイトルの場所まで戻す
        cameraObject.transform.DOMove(new Vector3(0, -15, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(stageManager.Reset);

        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();

        playerController.TitlePosMove();
        ballManager.BallReset();

        SetState(Scene.TITLE_INIT);
    }
}
