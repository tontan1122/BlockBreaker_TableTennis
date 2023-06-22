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

    [SerializeField, Header("�X�e�[�W�I���{�^��")]
    private Button[] selectButton;

    [SerializeField, Header("�J�����̃I�u�W�F�N�g")]
    private GameObject cameraObject;

    [SerializeField, Header("�N���X�Q��")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;

    private int currentLevel = 1;

    void Start()
    {
        //scene = Scene.TITLE_INIT;

        TitlePanel.SetActive(false);
        stageSelectPanel.SetActive(false);
        gamePanel.SetActive(false);

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
                    ballManager.SetStartPos(playerController.GetPlayerPosition());
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
                break;
            case Scene.STAGESELECT_END:
                stageSelectPanel.SetActive(false);
                SetState(Scene.GAME_INIT);
                break;
            case Scene.GAME_INIT:
                gamePanel.SetActive(true);
                cameraObject.transform.DOMove(new Vector3(0, 0, -10.0f), 1.0f)
                    .SetEase(Ease.InOutCubic);

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageGeneration(currentLevel);

                SetState(Scene.GAME);
                break;
            case Scene.GAME:
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition());
                }
                break;
            case Scene.GAME_END:

                break;
            case Scene.RESULT:
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
}
