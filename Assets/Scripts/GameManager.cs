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

    [SerializeField, Header("�X�e�[�W�Z���N�g�p�l��")]
    private GameObject stageSelectPanel;

    [SerializeField, Header("�Q�[���p�l��")]
    private GameObject gamePanel;

    [SerializeField, Header("���U���g�p�l��")]
    private GameObject resultPanel;

    [SerializeField, Header("�J�����̃I�u�W�F�N�g")]
    private GameObject cameraObject;

    [SerializeField, Header("�N���X�Q��:UI�֌W")]
    private SelectStageController selectStageController;
    [SerializeField]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;
    [SerializeField, Header("�N���X�Q��:�����֌W")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private CursorController cursorController;
    [SerializeField, Header("�N���X�Q�ƁF�����֌W")]
    private GameAudioManager audioManager;


    private int clickCount = 0;

    [SerializeField, Header("���݂̃��x��")]
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
                smoothBlinkingText.TextDisplay();       //�e�L�X�g�̕��V���̕\��
                clickCount = 0;
                SetState(Scene.TITLE);
                break;
            case Scene.TITLE:
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      //�{�[���𗣂������ʒu��ݒ�
                    ballManager.SetIsShot = playerController.GetIsControl;            //�{�[������Ă�悤�ɂ���
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
                currentLevel = selectStageController.LoadClearStage();  //�O��̃X�e�[�W�����ɂȂ�Ȃ��悤�Ƀf�[�^�����[�h
                selectStageController.CheakSelectPush(currentLevel);    //�{�^���̃I���I�t�X�V
                SetState(Scene.STAGESELECT);
                break;
            case Scene.STAGESELECT:
                ballManager.SetIsShot = playerController.GetIsControl;      //�{�[������Ă�悤�ɂ���
                //�{�^���������܂ł��̃V�[��
                break;
            case Scene.STAGESELECT_END:
                stageSelectPanel.SetActive(false);
                SetState(Scene.GAME_INIT);
                break;
            case Scene.GAME_INIT:
                gamePanel.SetActive(true);
                //�J�����̈ړ�
                cameraObject.transform.DOMove(new Vector3(0, stageManager.ContinuousClear * 15, -10.0f), 1.0f)
                    .SetEase(Ease.InOutCubic);

                gameUIController.ChangeStageText(currentLevel);     //�X�e�[�W���\�L�̍X�V
                gameUIController.MissCountText(0);  //�~�X�J�E���g�e�L�X�g�̃��Z�b�g

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageGeneration(currentLevel);     //�X�e�[�W����

                cursorController.CursorOff();

                SetState(Scene.GAME);
                break;
            case Scene.GAME:
                ballManager.SetIsShot = playerController.GetIsControl;//player�̏�ԂŃ{�[���𔭎˂ł��邩�ǂ������߂�

                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.R))    //���X�^�[�g
                {
                    stageManager.ResetBlocks();
                    ballManager.BallReset();
                    ballManager.MissCount++;
                    gameUIController.MissCountText(ballManager.MissCount);  //�~�X�J�E���g�e�L�X�g�̍X�V
                }

                if (stageManager.IsClear)
                {
                    audioManager.ClearStageAudio();
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) //�����~�X������
                {
                    ballManager.IsMiss = false;
                    gameUIController.MissCountText(ballManager.MissCount);   //�~�X�J�E���g�̕\��
                    stageManager.ResetBlocks(); //�u���b�N��z�u������
                }
                break;
            case Scene.GAME_END:
                stageManager.ClearBar();    //���ȂȂ��悤�ɏ��̔z�u
                cursorController.CursorOn();

                //�N���A�����X�e�[�W�̕ۑ�
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
                    stageManager.ResetBlocks(); //�X�e�[�W�͕ς����ɐ���
                    ballManager.BallReset();
                    resultPanel.SetActive(false);

                    stageManager.IsClear = false;   //�N���A���������Z�b�g

                    ballManager.MissCount = 0;
                    gameUIController.MissCountText(0);  //�~�X�J�E���g�e�L�X�g�̃��Z�b�g

                    Destroy(stageManager.GetCloneFloor);        //���̍폜

                    SetState(Scene.GAME);
                }

                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                    ballManager.SetIsShot = playerController.GetIsControl;      //�{�[������Ă�悤�ɂ���
                }
                //�{�^�����������܂�
                break;
        }
    }

    private void SetState(Scene state)
    {
        scene = state;
    }

    /// <summary>
    /// UI�̃{�^���ŌĂяo���֐�
    /// ���x���I���{�^��
    /// </summary>
    /// <param name="level">�I�񂾃��x��</param>
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

        //�J�������^�C�g���̏ꏊ�܂Ŗ߂�
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
