using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField, Header("ステージテキスト")]
    private TextMeshProUGUI stageText;

    [SerializeField, Header("ミス表示のテキスト")]
    private TextMeshProUGUI missText;

    void Start()
    {
        stageText.text = "Stage0";
    }

    public void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }

    public void ChengeMissCountText(int missCount)
    {
        missText.text = missCount.ToString() + "Miss";
    }
}
