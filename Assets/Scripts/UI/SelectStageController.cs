using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("ステージセレクトボタン")]
    private Button[] selectButton;

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
}
