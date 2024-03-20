using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// リザルトキャンバスについているリザルトUIの表示関連クラス
/// </summary>
public class ResultController : MonoBehaviour
{
    [SerializeField, Header("現在の最後のステージ番号")]
    private int finalStageNum;

    [SerializeField, Header("次のステージに移動するボタン")]
    private Button nextStageButton;

    [SerializeField, Header("全ステージクリア時のテキスト")]
    private TextMeshProUGUI finalText;

    [SerializeField,Header("最終ステージクリア時のコメント表示パネル")]
    private GameObject commentPanel;

    private void Start()
    {
        finalText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ステージが最後のステージかどうかを調べる
    /// </summary>
    /// <param name="level">現在のレベル</param>
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
