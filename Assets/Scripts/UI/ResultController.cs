using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���U���g�L�����o�X�ɂ��Ă��郊�U���gUI�̕\���֘A�N���X
/// </summary>
public class ResultController : MonoBehaviour
{
    [SerializeField, Header("���݂̍Ō�̃X�e�[�W�ԍ�")]
    private int finalStageNum;

    [SerializeField, Header("���̃X�e�[�W�Ɉړ�����{�^��")]
    private Button nextStageButton;

    [SerializeField, Header("�S�X�e�[�W�N���A���̃e�L�X�g")]
    private TextMeshProUGUI finalText;

    [SerializeField,Header("�ŏI�X�e�[�W�N���A���̃R�����g�\���p�l��")]
    private GameObject commentPanel;

    private void Start()
    {
        finalText.gameObject.SetActive(false);
    }

    /// <summary>
    /// �X�e�[�W���Ō�̃X�e�[�W���ǂ����𒲂ׂ�
    /// </summary>
    /// <param name="level">���݂̃��x��</param>
    public void CheckFinalStage(int level)
    {
        
        if(level >= finalStageNum)
        {
            finalText.gameObject.SetActive(true);

            if(Time.timeScale == 1.5f)
            {
                commentPanel.SetActive(false);
            }
            else
            {
                commentPanel.SetActive(true);
            }

            nextStageButton.interactable = false;
        }
        else
        {
            finalText.gameObject.SetActive(false);

            nextStageButton.interactable = true;
        }
    }
}
