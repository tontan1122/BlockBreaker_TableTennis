using System;
using System.Collections;
using UnityEngine;
using UniRx;

/// <summary>
/// �Q�[���̏�ԑJ��
/// </summary>
internal enum Scene
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

internal static class GlobalConst
{
    internal const int STAGE_SIZE_Y = 15;
    internal const float BALL_SIZE = 0.5f;
    internal static int heightUnavailableClick; // ��ʃN���b�N���ł��Ȃ��͈�
}

internal static class StaticVariable
{
    internal static bool isPosibleClickOperation = true;   // ��ʃN���b�N���ł��邩�ǂ���
}

/// <summary>
/// �Q�[���̊Ǘ��N���X
/// </summary>
internal class GameManager : MonoBehaviour
{
    internal Scene scene;

    [SerializeField, Header("�N���X�Q��:UI�֌W")]
    private UIManager uiManager;
    [SerializeField, Header("�N���X�Q��:�����֌W")]
    private StageManager stageManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField, Header("�N���X�Q�ƁF�����֌W")]
    private GameAudioManager audioManager;
    [SerializeField, Header("�N���X�Q�ƁF�f�[�^�֌W")]
    private ClearStageData clearStageData;

    [SerializeField, Header("���݂̃��x��")]
    private int currentLevel = 0;

    private int currentMaxLevel = 0;    // ���݂̍ō����x��
    private int clickCount = 0;         // �N���b�N��

    private bool isFirstPlay = true;    // �Q�[�����N�����čŏ��̃v���C���ǂ���
    private bool isHintPanelActive = false; // �q���g�p�l������x�\���������ǂ���
    private bool isStopGame = false;    // �{�[�����v���C���[�̏����g�����ǂ���

    private void Start()
    {
        SetClickArea(); // �N���b�N�ł���͈͂��w��

        // �|�[�Y���͒e��łĂȂ��悤�ɂ���
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
            audioManager.PlayGameSE(0); // �{�^���N���b�N����炷
        }

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
                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);      //�{�[���𗣂������ʒu��ݒ�
                }
                if (!isStopGame && !uiManager.GetAnyPanelActive())    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetIsShot = playerController.GetIsControl;            //�{�[������Ă�悤�ɂ���
                }
                if (uiManager.GetAnyPanelActive())
                {
                    ballManager.SetIsShot = false;
                }
                if (Input.GetMouseButtonDown(0) &&
                    !uiManager.GetAnyPanelActive() && 
                    Input.mousePosition.y <= GlobalConst.heightUnavailableClick)  // �ݒ��ʂ��o�Ă��邩���N���b�N�����ӏ������Ă��̍����ȏ�Ȃ�
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
                SetClickArea(); // �N���b�N�ł���͈͂��w��
                SetState(Scene.STAGESELECT_INIT);
                break;

            case Scene.STAGESELECT_INIT:
                currentMaxLevel = clearStageData.LoadClearStage();  //�O��̃X�e�[�W�����ɂȂ�Ȃ��悤�Ƀf�[�^�����[�h
                if (isFirstPlay)    //�ŏ��̃v���C�Ȃ�
                {
                    uiManager.StageSelectUI(true, currentMaxLevel, currentMaxLevel);    // ���݂̍ō����B�X�e�[�W�Ƀo�[���ړ�
                }
                else
                {
                    uiManager.StageSelectUI(true, currentMaxLevel, currentLevel);   // �ȑO�Ƀv���C�����X�e�[�W�Ƀo�[���ړ�
                }
                SetState(Scene.STAGESELECT);
                break;

            case Scene.STAGESELECT:

                ballManager.SetIsShot = playerController.GetIsControl;      //�{�[������Ă�悤�ɂ���
                //UI�̃{�^���������܂ł��̃V�[��
                break;

            case Scene.STAGESELECT_END:
                uiManager.StageSelectUI(false, currentLevel, currentLevel);
                isFirstPlay = false;
                SetState(Scene.GAME_INIT);
                break;

            case Scene.GAME_INIT:
                uiManager.GameUI(true);
                GlobalConst.heightUnavailableClick = Screen.height / 5 * 4; // ��ʃN���b�N���ł��Ȃ��͈͂��w��

                // �p�l����Active�ɂȂ����t���[�����ƃe�L�X�g�ύX���ł��Ȃ�����1�t���ҋ@
                StartCoroutine(DelayFrame(Time.deltaTime, () =>
                {
                    // 1�t���[����ɂ����̏��������s�����
                    uiManager.GameUI_ChangeStageText(currentLevel);     //�X�e�[�W���\�L�̍X�V
                    ballManager.MissCount = 0;
                    uiManager.GameUI_MissCountText(0);  //�~�X�J�E���g�e�L�X�g�̃��Z�b�g
                    isHintPanelActive = false;  // �q���g�p�l���̃��Z�b�g
                }));


                //�J�����̈ړ�
                cameraController.MoveNextStageCamera(stageManager.ContinuousClear);

                playerController.NextStageMove();
                ballManager.ResetTheBall();
                stageManager.StageInit(currentLevel);     //�X�e�[�W����

                SetState(Scene.GAME);
                break;

            case Scene.GAME:
                if (!PauseUIController.IsPaused && !isStopGame && !uiManager.GetAnyPanelActive())  // �|�[�Y���łȂ����
                {
                    ballManager.SetIsShot = playerController.GetIsControl;//player�̏�ԂŃ{�[���𔭎˂ł��邩�ǂ������߂�
                }
                if (uiManager.GetAnyPanelActive())
                {
                    ballManager.SetIsShot = false;
                }

                if (!ballManager.isMove)    //�����{�[���������Ă��Ȃ��Ȃ�
                {
                    ballManager.SetStartPos(playerController.GetPlayerPosition);
                }

                if (Input.GetKeyDown(KeyCode.R))    //���X�^�[�g
                {
                    stageManager.StageReset();
                    ballManager.RestartTheBall();
                    ballManager.MissCount++;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);  //�~�X�J�E���g�e�L�X�g�̍X�V
                }

                if (stageManager.IsClear)
                {
                    audioManager.PlayGameSE(2);
                    SetState(Scene.GAME_END);
                }
                if (ballManager.IsMiss) //�����~�X������
                {
                    ballManager.IsMiss = false;
                    uiManager.GameUI_MissCountText(ballManager.MissCount);   //�~�X�J�E���g�̕\��
                    ballManager.SetStartPos(playerController.GetPlayerPosition);    // �{�[���̐����ʒu�ݒ�
                    stageManager.StageReset(); //�u���b�N��z�u������
                }
                if (ballManager.MissCount == 10 && !isHintPanelActive)
                {
                    HintEvent();  // �~�X10��ڂ̃C�x���g
                    isHintPanelActive = true;
                }
                break;

            case Scene.GAME_END:
                stageManager.ClearStage();    //���ȂȂ��悤�ɏ��̔z�u

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
                    ballManager.RestartTheBall();

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
    internal void MoveGame(int level)
    {
        currentLevel = level;
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);
        SetState(Scene.STAGESELECT_END);
    }

    /// <summary>
    /// ���U���g�̃{�^������Ăяo�����
    /// </summary>
    internal void NextStage()
    {
        uiManager.ResultUI(false);

        currentLevel++;

        //SE
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);

        stageManager.StageClearReset();

        SetState(Scene.GAME_INIT);
    }

    /// <summary>
    /// ���U���g�̃{�^������Ăяo�����
    /// </summary>
    internal void MoveTitle()
    {
        uiManager.ResultUI(false);
        uiManager.GameUI(false);

        //SE
        audioManager.PlayGameSE(0);
        audioManager.PlayGameSE(1);

        //�J�������^�C�g���̏ꏊ�܂Ŗ߂�
        cameraController.MoveTitleCamera(stageManager.Reset);

        playerController.TitlePosMove();
        ballManager.ResetTheBall();
        ballManager.BackTitle();

        SetState(Scene.TITLE_INIT);
    }

    internal void RestartStage()
    {
        stageManager.StageReset();
        ballManager.RestartTheBall();
        ballManager.MissCount++;
        uiManager.GameUI_MissCountText(ballManager.MissCount);  //�~�X�J�E���g�e�L�X�g�̍X�V
    }

    /// <summary>
    /// ���U���g�ł̃X�e�[�W���X�^�[�g
    /// </summary>
    internal void ResultRestartStage()
    {
        BackGame();
        stageManager.StageReset(); //�X�e�[�W�͕ς����ɐ���
        ballManager.RestartTheBall();

        uiManager.ResultUI(false);

        stageManager.IsClear = false;   //�N���A���������Z�b�g

        ballManager.MissCount = 0;
        uiManager.GameUI_MissCountText(0);

        stageManager.ClearStageReset();             //���A�܂��͓V��Ȃǂ̍폜


        SetState(Scene.GAME);
    }

    /// <summary>
    /// �~�X10��Ńq���g�p�l�����o��
    /// �q���g�p�l���̓��U���g���烊�X�^�[�g�����ꍇ�͏o���Ȃ��悤�ɂ���
    /// </summary>
    private void HintEvent()
    {
        // �q���g�����߂�p�l����\��
        uiManager.GameUI_HintPanel(true);
        isStopGame = true;  // �Q�[�����ꎞ��~����
        ballManager.SetIsShot = false;  // �{�[����łĂȂ�����
    }

    public void BackGame()
    {
        // �Q�[���ɖ߂�Ƃ��Ƀ{�[��������Ȃ��悤�ɂ��邽�߂̏���
        ballManager.SetIsShot = false;
        isStopGame = true;  // �e�𔭎˂��Ȃ��悤�ɂ��邽��

        // 60F�҂��Ă���{�[������Ă�悤�ɂ���
        StartCoroutine(DelayFrame(60.0f, () =>
        {
            ballManager.SetIsShot = true;
            isStopGame = false; // �Q�[�����Đ�����
        }));
    }

    /// <summary>
    /// �ݒ��ʂ��J��
    /// </summary>
    internal void SettingPanelActive()
    {
        uiManager.SettingActive(true);
        isStopGame = true;  // �Q�[�����ꎞ��~����
        ballManager.SetIsShot = false;  // �{�[������ĂȂ��悤�ɂ���
    }

    private void SetClickArea()
    {
        GlobalConst.heightUnavailableClick = Screen.height / 6 * 5; // ��ʃN���b�N���ł��Ȃ��͈͂��w��
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
