using UnityEngine;
using TMPro;

internal class GameUIController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�e�L�X�g")]
    private TextMeshProUGUI stageText;

    [SerializeField, Header("�~�X�\���̃e�L�X�g")]
    private TextMeshProUGUI missText;

    void Start()
    {
        stageText.text = "Stage0";
    }

    internal void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }

    internal void ChengeMissCountText(int missCount)
    {
        missText.text = missCount.ToString() + "Miss";
    }
}
