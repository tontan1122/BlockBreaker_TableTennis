using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageController : MonoBehaviour
{
    [SerializeField, Header("ステージセレクトボタン")]
    private Button[] selectButton;

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
        selectButton[0].interactable|= true;    //ステージ1がいけないということはないため
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
