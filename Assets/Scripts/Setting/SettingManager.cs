using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class Setting
{
    public float BGMValue;
    public float SEValue;
}

/// <summary>
/// �ݒ�֘A�̂��̂��Ǘ�����N���X
/// </summary>
public class SettingManager : MonoBehaviour
{ 
    [SerializeField, Header("�ݒ��ʂ����{�^��")]
    private Button returnButton;

    [SerializeField, Header("�N���X�Q�ƌn")]
    private AudioVolume audioVolume;

    private void Awake()
    {
        Setting setting = LoadSettingData();

        audioVolume.AudioInit(setting.BGMValue, setting.SEValue);

        returnButton.onClick.AddListener(SettingAdaptation);
    }

    public void SettingAdaptation()     //�����Z�[�u����
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;

        SaveSettingData(setting);
    }

    public void SaveSettingData(Setting setting)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(setting);

        writer = new StreamWriter(Application.dataPath + "/datas/settingdata.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log($"�Z�[�u�����F{jsonstr}");
    }

    public Setting LoadSettingData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/datas/settingdata.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Setting>(datastr);
    }
}
