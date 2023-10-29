using UniRx;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// UI�̑S�̊Ǘ�
/// </summary>
internal class UIManager : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�Z���N�g�p�l��")]
    private GameObject stageSelectPanel;

    [SerializeField, Header("�Q�[���p�l��")]
    private GameObject gamePanel;

    [SerializeField, Header("���U���g�p�l��")]
    private GameObject resultPanel;

    [SerializeField, Header("�q���g�����p�l��")]
    private GameObject hintPanel;

    [SerializeField, Header("�ݒ�p�l��")]
    private GameObject settingPanel;

    [SerializeField, Header("�N���X�Q��:UI�֌W")]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private SelectStageController selectStageController;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;

    [SerializeField, Header("UI�A�j���[�V����")]
    private PanelActiveAnimation[] panelActiveAnimation;

    private void Start()
    {
        stageSelectPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);
        hintPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    /// <summary>
    /// �^�C�g����UI����
    /// </summary>
    internal void TitleUI()
    {
        smoothBlinkingText.TextDisplay();       //�e�L�X�g�̕��V���̕\��
    }

    /// <summary>
    /// �X�e�[�W�Z���N�g��UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    /// <param name="level">�ۑ�����Ă���ō����x��</param>
    /// <param name="movingStageNumber">�ړ���̃X�e�[�W�ԍ�</param>
    internal void StageSelectUI(bool panelActive, int level, int movingStageNumber)
    {
        if (panelActive)
        {
            panelActiveAnimation[0].Open();
        }
        else
        {
            panelActiveAnimation[0].Close();
        }
        if (stageSelectPanel.activeSelf)    // �p�l�����\������Ă���Ȃ�
        {
            selectStageController.CheakSelectPush(level);    //�{�^���̃I���I�t�X�V
            selectStageController.SetScrollPosition(movingStageNumber);
        }
    }

    /// <summary>
    /// �Q�[����UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    internal void GameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// �~�X�̕\���e�L�X�g�̕ύX
    /// </summary>
    /// <param name="missCount"></param>
    internal void GameUI_MissCountText(int missCount)
    {
        gameUIController.MissCountText(missCount);  // �~�X�J�E���g�̃e�L�X�g�ύX
    }

    /// <summary>
    /// �X�e�[�W���̃e�L�X�g�ύX
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    internal void GameUI_ChangeStageText(int level)
    {
        gameUIController.ChangeStageText(level);    // �X�e�[�W���̃e�L�X�g�ύX
    }

    internal void GameUI_HintPanel(bool active)
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
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    internal void ResultUI(bool panelActive)
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
    /// �Ō�̃X�e�[�W���ǂ������f
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    internal void ResultUI_CheckStage(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }

    /// <summary>
    /// �ݒ�p�l���̃A�N�e�B�u
    /// </summary>
    internal void SettingActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[3].Open();
        }
        else
        {
            panelActiveAnimation[3].Close();
        }
        smoothBlinkingText.SettingActiveCheck(active);
    }

    internal bool getSettingActive
    {
        get { return settingPanel.activeSelf; }
    }
}
