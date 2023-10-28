using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
internal class Setting
{
    public float BGMValue;
    public float SEValue;
}

/// <summary>
/// �ݒ�֘A�̂��̂��Ǘ�����N���X
/// </summary>
internal class SettingManager : MonoBehaviour
{ 
    [SerializeField, Header("�ݒ��ʂ����{�^��")]
    private Button returnButton;

    [SerializeField, Header("�N���X�Q�ƌn")]
    private AudioVolume audioVolume;

    [SerializeField, Header("BGM�̏����ݒ�")]
    private int firstBGMValue = 60;

    [SerializeField, Header("SE�̏����ݒ�")]
    private int firstSEValue = 75;

    private string filePath; // �t�@�C���̃p�X���w��

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/settingdata.json";
        if (File.Exists(filePath))
        {
            Debug.Log("JSON�t�@�C�������݂��܂��B");
        }
        else
        {
            Debug.Log("JSON�t�@�C�������݂��܂���B");
            Setting firstSetting = new Setting();
            firstSetting.BGMValue = firstBGMValue;
            firstSetting.SEValue = firstSEValue;

            string jsonstr = JsonUtility.ToJson(firstSetting);
            File.WriteAllText(filePath, jsonstr);
            Debug.Log("�t�@�C���̐����ɐ������܂���");
        }

        Setting setting = LoadSettingData();

        audioVolume.AudioInit(setting.BGMValue, setting.SEValue);

        returnButton.onClick.AddListener(SettingAdaptation);
    }

    internal void SettingAdaptation()     //�����Z�[�u����
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;

        SaveSettingData(setting);
    }

    internal void SaveSettingData(Setting setting)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(setting);

        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log($"�Z�[�u�����F{jsonstr}");
    }

    internal Setting LoadSettingData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(filePath);
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Setting>(datastr);
    }
}
