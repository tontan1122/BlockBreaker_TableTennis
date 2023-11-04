using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
internal class Setting
{
    public float BGMValue;
    public float SEValue;
    public bool isHardMode;
}

/// <summary>
/// 設定関連のものを管理するクラス
/// </summary>
internal class SettingManager : MonoBehaviour
{ 
    [SerializeField, Header("設定画面を閉じるボタン")]
    private Button returnButton;

    [SerializeField, Header("クラス参照系")]
    private AudioVolume audioVolume;
    [SerializeField]
    private TimeManager timeManager;

    [SerializeField, Header("BGMの初期設定")]
    private int firstBGMValue = 60;

    [SerializeField, Header("SEの初期設定")]
    private int firstSEValue = 75;

    [SerializeField, Header("チェックボックス")]
    private Toggle hardModeCheckBox;

    private string filePath; // ファイルのパスを指定

    private void Awake()
    {
        FileCheck();

        Setting setting = LoadSettingData();

        audioVolume.AudioInit(setting.BGMValue, setting.SEValue);
        hardModeCheckBox.isOn = setting.isHardMode;
        timeManager.TimeChenger(hardModeCheckBox.isOn);

        hardModeCheckBox.onValueChanged.AddListener(timeManager.TimeChenger);   // 裏卓球部モードの設定
        returnButton.onClick.AddListener(SettingAdaptation);
    }

    private void FileCheck()
    {
        filePath = Application.persistentDataPath + "/settingdata.json";
        if (File.Exists(filePath))
        {
            Debug.Log("JSONファイルが存在します。");
        }
        else
        {
            Debug.Log("JSONファイルが存在しません。");
            Setting firstSetting = new Setting();
            firstSetting.BGMValue = firstBGMValue;
            firstSetting.SEValue = firstSEValue;
            firstSetting.isHardMode = false;

            string jsonstr = JsonUtility.ToJson(firstSetting);
            File.WriteAllText(filePath, jsonstr);
            Debug.Log("ファイルの生成に成功しました");
        }
    }

    internal void SettingAdaptation()     //情報をセーブする
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;
        setting.isHardMode = hardModeCheckBox.isOn;

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
        Debug.Log($"セーブ完了：{jsonstr}");
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
