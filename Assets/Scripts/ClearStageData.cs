using UnityEngine;

/// <summary>
/// クリアしたステージのセーブ、ロード
/// </summary>
public class ClearStageData : MonoBehaviour
{
    internal int LoadClearStage()
    {
        return PlayerPrefs.GetInt("ClearStage", 0);
    }

    internal void SaveClearStage(int stageLevel)
    {
        int dataStage = PlayerPrefs.GetInt("ClearStage");
        if (stageLevel > dataStage)
        {
            PlayerPrefs.SetInt("ClearStage", stageLevel);
        }
    }
}
