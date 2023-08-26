using UnityEngine;

public class UIManager : MonoBehaviour
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
    public void TitleUI()
    {
        smoothBlinkingText.TextDisplay();       //�e�L�X�g�̕��V���̕\��
    }

    /// <summary>
    /// �ݒ�p�l���̃A�N�e�B�u
    /// </summary>
    public void SettingActive(bool active)
    {
        settingPanel.SetActive(active);
        smoothBlinkingText.SettingActiveCheck(active);
    }

    public bool getSettingActive
    {
        get { return settingPanel.activeSelf; }
    }

    /// <summary>
    /// �X�e�[�W�Z���N�g��UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    /// <param name="level">�ۑ�����Ă���ō����x��</param>
    public void StageSelectUI(bool panelActive, int level)
    {
        stageSelectPanel.SetActive(panelActive);
        if (stageSelectPanel.activeSelf)    // �p�l�����\������Ă���Ȃ�
        {
            selectStageController.CheakSelectPush(level);    //�{�^���̃I���I�t�X�V
        }
    }

    /// <summary>
    /// �Q�[����UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    public void GameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// �~�X�̕\���e�L�X�g�̕ύX
    /// </summary>
    /// <param name="missCount"></param>
    public void GameUI_MissCountText(int missCount)
    {
        gameUIController.MissCountText(missCount);  // �~�X�J�E���g�̃e�L�X�g�ύX
    }

    /// <summary>
    /// �X�e�[�W���̃e�L�X�g�ύX
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    public void GameUI_ChangeStageText(int level)
    {
        gameUIController.ChangeStageText(level);    // �X�e�[�W���̃e�L�X�g�ύX
    }

    public void GameUI_HintPanel(bool active)
    {
        hintPanel.SetActive(active);
    }

    /// <summary>
    /// ���U���g��UI����
    /// </summary>
    /// <param name="panelActive">�p�l���̕\���A��\��</param>
    public void ResultUI(bool panelActive)
    {
        resultPanel.SetActive(panelActive);
    }

    /// <summary>
    /// �Ō�̃X�e�[�W���ǂ������f
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    public void ResultUI_CheckStage(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }
}
