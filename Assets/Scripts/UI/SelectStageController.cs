using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�Z���N�g�{�^��")]
    private Button[] selectButton;

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
}
