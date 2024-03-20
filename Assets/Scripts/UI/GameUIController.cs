using UnityEngine;
using TMPro;

/// <summary>
/// ゲーム内UIの操作クラス
/// </summary>
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

    /// <summary>
    /// ステージ数表記の変更
    /// </summary>
    /// <param name="level">レベル番号</param>
    public void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }

    /// <summary>
    /// ミス数表記の変更
    /// </summary>
    /// <param name="missCount">ミス数</param>
    public void ChengeMissCountText(int missCount)
    {
        missText.text = missCount.ToString() + "Miss";
    }
}
