using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�Z���N�g�{�^��")]
    private Button[] selectButton;

    [SerializeField, Header("�X�N���[���o�[�̈��Ɋ܂ރ{�^���̌�")]
    private int columnButtonNumber = 5;

    [SerializeField, Header("ScrollRectComponent")]
    private ScrollRect stageScrollRect;

    private void Start()
    {
        //�X�e�[�W�̃{�^���̃e�L�X�g���X�e�[�W�ԍ��ɕς���
        for (int i = 0; i < selectButton.Length; i++)
        {
            GameObject childText = selectButton[i].gameObject.transform.GetChild(0).gameObject;
            childText.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }


    }

    /// <summary>
    /// StageSelect�Ń{�^���������邩�ǂ�����ύX����֐�
    /// </summary>
    /// <param name="clearStageValue">�N���A�����X�e�[�W</param>
    /// <returns></returns>
    public bool CheakSelectPush(int clearStageValue)
    {
        for (int i = 0; i < selectButton.Length; i++)
        {
            if (i > clearStageValue)    //���̃X�e�[�W�܂��̓N���A�����X�e�[�W�ȊO�͉����Ȃ�����
            {
                selectButton[i].interactable = false;
            }
            else
            {
                selectButton[i].interactable = true;
            }
        }
        selectButton[0].interactable = true;    //�X�e�[�W1�������Ȃ��Ƃ������Ƃ͂Ȃ�����
        return true;
    }

    /// <summary>
    /// �X�N���[���o�[�̈ʒu�����߂�
    /// </summary>
    /// <param name="movingStageNumber">�ړ��������X�e�[�W�ԍ�</param>
    public void SetScrollPosition(float movingStageNumber)
    {
        // ���݂̃N���A���Ă���X�e�[�W�̃{�^�����ǂ̗�ɂ��邩
        float clearStagecolumn = movingStageNumber / columnButtonNumber;
        clearStagecolumn = Mathf.Floor(clearStagecolumn); // �����_�؂�̂�
        if (movingStageNumber % columnButtonNumber == 0 && movingStageNumber != 0)
        {
            clearStagecolumn -= 1.0f; // ��񕪏�ɏグ��
        }

        float currentColumnNumber = ((float)selectButton.Length - columnButtonNumber) / columnButtonNumber;    // ���݂̗�
        currentColumnNumber = Mathf.Ceil(currentColumnNumber);  // �����_�̐؂�グ

        float columnNumber = 1.0f / currentColumnNumber;   // ���𓮂�����
        float scrollPos = 1.0f - (columnNumber * (float)clearStagecolumn); // �X�N���[���̈ʒu�����߂�

        stageScrollRect.verticalNormalizedPosition = scrollPos; // �ʒu��ݒ�
    }
}
