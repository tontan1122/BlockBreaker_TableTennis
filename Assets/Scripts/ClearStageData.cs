using UnityEngine;

/// <summary>
/// �N���A�����X�e�[�W�̃Z�[�u�A���[�h
/// </summary>
public class ClearStageData : MonoBehaviour
{
    /// <summary>
    /// �N���A���Ă���X�e�[�W�̃��[�h
    /// </summary>
    /// <returns>�N���A���Ă���X�e�[�W�ԍ�</returns>
    public int LoadClearStage()
    {
        return PlayerPrefs.GetInt("ClearStage", 0);
    }

    /// <summary>
    /// �N���A���Ă���X�e�[�W��ۑ�����
    /// </summary>
    /// <param name="stageLevel">�N���A���Ă���X�e�[�W�̔ԍ�</param>
    public void SaveClearStage(int stageLevel)
    {
        int dataStage = PlayerPrefs.GetInt("ClearStage");
        if (stageLevel > dataStage)
        {
            PlayerPrefs.SetInt("ClearStage", stageLevel);
        }
    }
}
