using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// UI�̑S�̊Ǘ�
/// </summary>
internal class UIManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���p�l��")]
    private GameObject gamePanel;

    [SerializeField, Header("���U���g�p�l��")]
    private GameObject resultPanel;

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

        for (int i = 0; i < panelActiveAnimation.Length; i++)
        {
            panelActiveAnimation[i].gameObject.SetActive(false);
        }
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
            selectStageController.SetScrollPosition(movingStageNumber); // �{�^���̈ʒu����
            selectStageController.CheakSelectPush(level);    //�{�^���̃I���I�t�X�V
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
        smoothBlinkingText.QuitActiveCheck(active);
    }

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

    internal bool GetAnyPanelActive()
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
