using UnityEngine;
using TMPro;

internal class GameUIController : MonoBehaviour
{
    [SerializeField, Header("ステージテキスト")]
    private TextMeshProUGUI stageText;

    [SerializeField, Header("ミス表示のテキスト")]
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
