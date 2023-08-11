using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// �Q�[���̏�ԑJ��
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
/// �Q�[���̊Ǘ��N���X
/// </summary>
public class GameManager : MonoBehaviour
{
    public Scene scene;

    [SerializeField, Header("�N���X�Q��:UI�֌W")]
    private UIManager uiManager;
    [SerializeField, Header("�N���X�Q��:�����֌W")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private CursorController cursorController;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField, Header("�N���X�Q�ƁF�����֌W")]
    private GameAudioManager audioManager;
    [SerializeField, Header("�N���X�Q�ƁF�f�[�^�֌W")]
    private ClearStageData clearStageData;


    private int clickCount = 0;

    [SerializeField, Header("���݂̃��x��")]
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
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      //�{�[���𗣂������ʒu��ݒ�
                    ballManager.SetIsShot = playerController.GetIsControl;            //�{�[������Ă�悤�ɂ���
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
                currentLevel = clearStageData.LoadClearStage();  //�O��̃X�e�[�W�����ɂȂ�Ȃ��悤�Ƀf�[�^�����[�h
                uiManager.StageSelectUI(true, currentLevel);
                SetState(Scene.STAGESELECT);
                break;

            case Scene.STAGESELECT:
                ballManager.SetIsShot = playerController.GetIsControl;      //�{�[������Ă�悤�ɂ���
                //UI�̃{�^���������܂ł��̃V�[��
                break;

            case Scene.STAGESELECT_END:
                uiManager.StageSelectUI(false, currentLevel);
                SetState(Scene.GAME_INIT);
                break;

            case Scene.GAME_INIT:
                uiManager.GameUI(true);

                // �p�l����Active�ɂȂ����t���[�����ƃe�L�X�g�ύX���ł��Ȃ�����1�t���ҋ@
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    // 1�t���[����ɂ����̏��������s�����
                    uiManager.GameUI_ChangeStageText(currentLevel);     //�X�e�[�W���\�L�̍X�V
                    uiManager.GameUI_MissCountText(0);  //�~�X�J�E���g�e�L�X�g�̃��Z�b�g
                }));


                //�J�����̈ړ�
                cameraController.MoveNextStageCamera(stageManager.ContinuousClear);

                playerController.NextStageMove();
                ballManager.BallReset();
                stageManager.StageInit(currentLevel);     //�X�e�[�W����

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
                    stageManager.StageReset();
                    ballManager.BallRestart();
                    ballManager.MissCount++;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);  //�~�X�J�E���g�e�L�X�g�̍X�V
                }
                if (Input.GetKeyDown(KeyCode.H))    // �q���g�X�^�[�g
                {
                    stageManager.HintClick();
                }

                if (stageManager.IsClear)
                {
                    audioManager.ClearStageAudio();
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) //�����~�X������
                {
                    ballManager.IsMiss = false;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);   //�~�X�J�E���g�̕\��
                    stageManager.StageReset(); //�u���b�N��z�u������
                }
                break;

            case Scene.GAME_END:
                stageManager.ClearStage();    //���ȂȂ��悤�ɏ��̔z�u
                cursorController.CursorOn();

                //�N���A�����X�e�[�W�̕ۑ�
                clearStageData.SaveClearStage(currentLevel);

                SetState(Scene.RESULT_INIT);

                break;

            case Scene.RESULT_INIT:
                uiManager.ResultUI(true);
                // �p�l����Active�ɂȂ����t���[�����ƃe�L�X�g�ύX���ł��Ȃ�����1�t���ҋ@
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    uiManager.ResultUI_CheckStage(currentLevel);
                }));

                SetState(Scene.RESULT);
                break;

            case Scene.RESULT:
                if (Input.GetKeyDown(KeyCode.R))    // ���X�^�[�g
                {
                    stageManager.StageReset(); //�X�e�[�W�͕ς����ɐ���
                    ballManager.BallRestart();

                    uiManager.ResultUI(false);

                    stageManager.IsClear = false;   //�N���A���������Z�b�g

                    ballManager.MissCount = 0;
                    uiManager.GameUI_MissCountText(0);

                    stageManager.ClearStageReset();             //���A�܂��͓V��Ȃǂ̍폜

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

    /// <summary>
    /// ���U���g�̃{�^������Ăяo�����
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
    /// ���U���g�̃{�^������Ăяo�����
    /// </summary>
    public void MoveTitle()
    {
        uiManager.ResultUI(false);
        uiManager.GameUI(false);

        //SE
        audioManager.ClickButtonAudio();
        audioManager.MoveStageAudio();

        //�J�������^�C�g���̏ꏊ�܂Ŗ߂�
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
    /// �n���ꂽ�t���[����ҋ@����
    /// </summary>
    private IEnumerator DelayFrame(float frames, Action action)
    {
        yield return frames;
        action?.Invoke();
    }
}
