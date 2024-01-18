using UnityEngine;

/// <summary>
/// クリアしたステージのセーブ、ロード
/// </summary>
public class ClearStageData : MonoBehaviour
{
    /// <summary>
    /// クリアしているステージのロード
    /// </summary>
    /// <returns>クリアしているステージ番号</returns>
    public int LoadClearStage()
    {
        return PlayerPrefs.GetInt("ClearStage", 0);
    }

    /// <summary>
    /// クリアしているステージを保存する
    /// </summary>
    /// <param name="stageLevel">クリアしているステージの番号</param>
    public void SaveClearStage(int stageLevel)
    {
        int dataStage = PlayerPrefs.GetInt("ClearStage");
        if (stageLevel > dataStage)
        {
            PlayerPrefs.SetInt("ClearStage", stageLevel);
        }
    }
}
