using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("ステージセレクトボタン")]
    private Button[] selectButton;

    [SerializeField, Header("スクロールバーの一列に含むボタンの個数")]
    private int columnButtonNumber = 5;

    [SerializeField, Header("ScrollRectComponent")]
    private ScrollRect stageScrollRect;

    private void Start()
    {
        //ステージのボタンのテキストをステージ番号に変える
        for (int i = 0; i < selectButton.Length; i++)
        {
            GameObject childText = selectButton[i].gameObject.transform.GetChild(0).gameObject;
            childText.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }


    }

    /// <summary>
    /// StageSelectでボタンを押せるかどうかを変更する関数
    /// </summary>
    /// <param name="clearStageValue">クリアしたステージ</param>
    /// <returns></returns>
    public bool CheakSelectPush(int clearStageValue)
    {
        for (int i = 0; i < selectButton.Length; i++)
        {
            if (i > clearStageValue)    //次のステージまたはクリアしたステージ以外は押せなくする
            {
                selectButton[i].interactable = false;
            }
            else
            {
                selectButton[i].interactable = true;
            }
        }
        selectButton[0].interactable = true;    //ステージ1がいけないということはないため
        return true;
    }

    /// <summary>
    /// スクロールバーの位置を決める
    /// </summary>
    /// <param name="movingStageNumber">移動したいステージ番号</param>
    public void SetScrollPosition(float movingStageNumber)
    {
        // 現在のクリアしているステージのボタンがどの列にあるか
        float clearStagecolumn = movingStageNumber / columnButtonNumber;
        clearStagecolumn = Mathf.Floor(clearStagecolumn); // 小数点切り捨て
        if (movingStageNumber % columnButtonNumber == 0 && movingStageNumber != 0)
        {
            clearStagecolumn -= 1.0f; // 一列分上に上げる
        }

        float currentColumnNumber = ((float)selectButton.Length - columnButtonNumber) / columnButtonNumber;    // 現在の列数
        currentColumnNumber = Mathf.Ceil(currentColumnNumber);  // 小数点の切り上げ

        float columnNumber = 1.0f / currentColumnNumber;   // 一列を動かす幅
        float scrollPos = 1.0f - (columnNumber * (float)clearStagecolumn); // スクロールの位置を求める

        stageScrollRect.verticalNormalizedPosition = scrollPos; // 位置を設定
    }
}
