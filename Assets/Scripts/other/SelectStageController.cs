using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�Z���N�g�{�^��")]
    private Button[] selectButton;

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
        selectButton[0].interactable|= true;    //�X�e�[�W1�������Ȃ��Ƃ������Ƃ͂Ȃ�����
        return true;
    }

    public int LoadClearStage()
    {
        return PlayerPrefs.GetInt("ClearStage", 0);
    }

    public void SaveClearStage(int stageLevel)
    {
        PlayerPrefs.SetInt("ClearStage", stageLevel);
    }
}
