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
/// 設定関連のものを管理するクラス
/// </summary>
public class SettingManager : MonoBehaviour
{ 
    [SerializeField, Header("設定画面を閉じるボタン")]
    private Button returnButton;

    [SerializeField, Header("クラス参照系")]
    private AudioVolume audioVolume;

    private void Awake()
    {
        Setting setting = LoadSettingData();

        audioVolume.AudioInit(setting.BGMValue, setting.SEValue);

        returnButton.onClick.AddListener(SettingAdaptation);
    }

    public void SettingAdaptation()     //情報をセーブする
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
        Debug.Log($"セーブ完了：{jsonstr}");
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
