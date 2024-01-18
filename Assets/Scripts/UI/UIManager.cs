using UnityEngine;

/// <summary>
/// UI�̑S�̊Ǘ�
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���p�l��")]
    private GameObject gamePanel;

    [SerializeField, Header("���U���g�p�l��")]
    private GameObject resultPanel;

    [SerializeField, Header("���싅���̃C���[�W")]
    private GameObject hardModeImage;

    [SerializeField, Header("�N���X�Q��:UI�֌W")]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private SelectStageController selectStageController;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;
    [SerializeField]
    private PauseUIController pauseUIController;

    [SerializeField, Header("UI�A�j���[�V����")]
    private PanelActiveAnimation[] panelActiveAnimation;

    private void Start()
    {
        gamePanel.SetActive(false);
        hardModeImage.SetActive(false);

        for (int i = 0; i < panelActiveAnimation.Length; i++)
        {
            panelActiveAnimation[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        DisplayHardodeImage();
    }

    /// <summary>
    /// �^�C�g����UI����
    /// </summary>
    public void OperateTitleUI()
    {
        smoothBlinkingText.DisplayText();       //�e�L�X�g�̕��V���̕\��
    }

    /// <summary>
    /// �^�C�g���̃e�L�X�g�̕ύX
    /// </summary>
    public void ChangeTitleText()
    {
        smoothBlinkingText.ChangeText();
    }

    /// <summary>
    /// ���̕\��������
    /// </summary>
    private void DisplayHardodeImage()
    {
        if (Time.timeScale == 1.5f)
        {
            if (!hardModeImage.activeSelf)
            {
                hardModeImage.SetActive(true);
            }
        }
        else
        {
            if (hardModeImage.activeSelf)
            {
                hardModeImage.SetActive(false);
            }
        }
    }

    /// <summary>
    /// �X�e�[�W�Z���N�g��UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    /// <param name="level">�ۑ�����Ă���ō����x��</param>
    /// <param name="movingStageNumber">�ړ���̃X�e�[�W�ԍ�</param>
    public void OperateStageSelectUI(bool panelActive, int level, int movingStageNumber)
    {
        if (panelActive)
        {
            panelActiveAnimation[0].Open();
            selectStageController.SetScrollPosition(movingStageNumber); // �{�^���̈ʒu����
            selectStageController.ChangeButtonInteractivity(level);    //�{�^���̃I���I�t�X�V
        }
        else
        {
            panelActiveAnimation[0].Close();
        }
    }

    /// <summary>
    /// �Q�[����UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    public void OperateGameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// �~�X�̕\���e�L�X�g�̕ύX
    /// </summary>
    /// <param name="missCount"></param>
    public void OperateMissCountText(int missCount)
    {
        gameUIController.ChengeMissCountText(missCount);  // �~�X�J�E���g�̃e�L�X�g�ύX
    }

    /// <summary>
    /// �X�e�[�W���̃e�L�X�g�ύX
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    public void OperateStageLevelText(int level)
    {
        gameUIController.ChangeStageText(level);    // �X�e�[�W���̃e�L�X�g�ύX
    }

    /// <summary>
    /// �q���g�p�l���̕\���A��\��
    /// </summary>
    /// <param name="active">�p�l����\�����邩�ǂ���</param>
    public void SwitchHintPanelVisibility(bool active)
    {
        if (active)
        {
            panelActiveAnimation[2].Open();
        }
        else
        {
            panelActiveAnimation[2].Close();
        }
    }

    /// <summary>
    /// ���U���g��UI����
    /// </summary>
    /// <param name="panelActive">�p�l����\�����邩�ǂ���</param>
    public void SwitchResultPanelVisibility(bool panelActive)
    {
        if (panelActive)
        {
            panelActiveAnimation[1].Open();
        }
        else
        {
            panelActiveAnimation[1].Close();
        }
    }

    /// <summary>
    /// �ǂ̃X�e�[�W���̊m�F
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    public void CheckStageLevel(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }

    /// <summary>
    /// �ݒ�p�l���̃A�N�e�B�u
    /// </summary>
    public void SettingActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[3].Open();
        }
        else
        {
            panelActiveAnimation[3].Close();
        }
        smoothBlinkingText.SetSettingActive(active);
    }

    /// <summary>
    /// �|�[�Y�p�l����\���A��\������
    /// </summary>
    /// <param name="active">�|�[�Y���邩�ǂ���</param>
    public void PausePanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[4].Open();
            pauseUIController.Pause();
        }
        else
        {
            panelActiveAnimation[4].Close();
            pauseUIController.Resume();
        }
    }

    /// <summary>
    /// �I���m�F��ʂ̕\���A��\��
    /// </summary>
    /// <param name="active">�\�����邩�ǂ���</param>
    public void QuitGamePanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[5].Open();
        }
        else
        {
            panelActiveAnimation[5].Close();
        }
        smoothBlinkingText.SetQuitActive(active);
    }

    /// <summary>
    /// �N���W�b�g�p�l���̕\���A��\��
    /// </summary>
    /// <param name="active">�\�����邩�ǂ���</param>
    public void CreditPanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[6].Open();
        }
        else
        {
            panelActiveAnimation[6].Close();
        }
    }

    /// <summary>
    /// ��������̃p�l�����\������Ă��邩�ǂ���
    /// </summary>
    /// <returns>��������̃p�l�����\������Ă��邩�ǂ���</returns>
    public bool GetAnyPanelActive()
    {
        bool value = false;
        for (int i = 0; i < panelActiveAnimation.Length; i++)
        {
            if (panelActiveAnimation[i].gameObject.activeSelf)
            {
                value = true;
            }
        }
        return value;
    }
}
