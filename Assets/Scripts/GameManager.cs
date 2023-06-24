using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField, Header("�^�C�g���p�l��")]
    private GameObject TitlePanel;

    [SerializeField, Header("�X�e�[�W�Z���N�g�p�l��")]
    private GameObject stageSelectPanel;

    [SerializeField, Header("�Q�[���p�l��")]
    private GameObject gamePanel;

    [SerializeField, Header("���U���g�p�l��")]
    private GameObject resultPanel;

    [SerializeField, Header("�J�����̃I�u�W�F�N�g")]
    private GameObject cameraObject;

    [SerializeField, Header("�N���X�Q��")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private ResultController resultController;

    [SerializeField, Header("���݂̃��x��")]
    private int currentLevel = 1;

    void Start()
    {
        //scene = Scene.TITLE_INIT;

        TitlePanel.SetActive(false);
        stageSelectPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);

    }

    void Update()
    {
        switch (scene)
        {
            case Scene.TITLE_INIT:
                TitlePanel.SetActive(true);
                SetState(Scene.TITLE);
                break;
            case Scene.TITLE:
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetState(Scene.TITLE_END);
                }
                break;
            case Scene.TITLE_END:
                TitlePanel.SetActive(false);
                SetState(Scene.STAGESELECT_INIT);
                break;
            case Scene.STAGESELECT_INIT:
                stageSelectPanel.SetActive(true);
                SetState(Scene.STAGESELECT);
                break;
            case Scene.STAGESELECT:
                //�{�^���������܂ł��̃V�[��
                //MoveGame�֐����Ă΂��܂�
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

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageGeneration(currentLevel);

                SetState(Scene.GAME);
                break;
            case Scene.GAME:
                //�{�[���𔭎˂ł��Ȃ��悤�ɂ���
                ballManager.SetIsShot = playerController.GetIsControl;

                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    stageManager.ResetBlocks();
                    ballManager.BallReset();
                }

                if (stageManager.IsClear)
                {

                    SetState(Scene.GAME_END);
                }
                break;
            case Scene.GAME_END:
                gamePanel.SetActive(false);

                SetState(Scene.RESULT_INIT);

                break;
            case Scene.RESULT_INIT:
                resultPanel.SetActive(true);
                resultController.CheckFinalStage(currentLevel);
                SetState(Scene.RESULT);
                break;
            case Scene.RESULT:
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
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
        level--;
        currentLevel = level;
        SetState(Scene.STAGESELECT_END);
    }

    public void NextStage()
    {
        resultPanel.SetActive(false);
        currentLevel++;
        stageManager.StageMove();
        ballManager.BallReset();
        SetState(Scene.GAME_INIT);
    }

    public void MoveTitle()
    {
        cameraObject.transform.DOMove(new Vector3(0, -15, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(stageManager.Reset);
        //���[�V�����I������牺�̃��Z�b�g�֐��Ă�ł���������

        playerController.TitlePosMove();
        ballManager.BallReset();
        resultPanel.SetActive(false);
        SetState(Scene.TITLE_INIT);
    }
}
