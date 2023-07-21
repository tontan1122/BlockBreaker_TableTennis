using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// リザルトキャンバスについているリザルトUIの表示関連クラス
/// </summary>
public class ResultController : MonoBehaviour
{
    [SerializeField, Header("現在の最後のステージ番号")]
    private int finalStageNum = 5;

    [SerializeField, Header("次のステージに移動するボタン")]
    private Button nextStageButton;

    [SerializeField, Header("全ステージクリア時のテキスト")]
    private TextMeshProUGUI finalText;


    private void Start()
    {
        finalText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ステージが最後のステージかどうかを調べる
    /// </summary>
    /// <param name="level"></param>
    public void CheckFinalStage(int level)
    {
        
        if(level >= finalStageNum)
        {
            finalText.gameObject.SetActive(true);

            nextStageButton.interactable = false;
        }
        else
        {
            finalText.gameObject.SetActive(false);

            nextStageButton.interactable = true;
        }
    }
}
