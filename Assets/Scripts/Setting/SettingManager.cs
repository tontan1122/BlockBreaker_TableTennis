using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class Setting
{
    public float BGMValue;
    public float SEValue;
    public bool IsHardMode;
    public int LanguageNumber = 0;
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
    [SerializeField]
    private LanguageSelector languageSelector;

    [SerializeField, Header("チェックボックス")]
    private Toggle hardModeCheckBox;

    private string filePath; // ファイルのパスを指定
    private TimeManager timeManager;

    private static readonly int INIT_BGM_VALUE = 65;  // 起動時BGMの音量
    private static readonly int INIT_SE_VALUE = 70;  // 起動時SEの音量

    private void Awake()
    {
        CheckingFiles();

        Setting setting = LoadSettingData();

        timeManager = new TimeManager();

        audioVolume.InitialAudioSettings(setting.BGMValue, setting.SEValue);
        hardModeCheckBox.isOn = setting.IsHardMode;
        timeManager.ChengeTimeScale(hardModeCheckBox.isOn);
        languageSelector.PushChangeLanguageButton(setting.LanguageNumber);

        hardModeCheckBox.onValueChanged.AddListener(timeManager.ChengeTimeScale);   // 裏卓球部モードの設定
        returnButton.onClick.AddListener(SaveSettings);
    }

    /// <summary>
    /// ファイルの確認
    /// </summary>
    private void CheckingFiles()
    {
        filePath = Application.persistentDataPath + "/settingdata.json";
        if (File.Exists(filePath))  // JSONファイルが存在するかどうか
        {
        }
        else
        {
            Setting firstSetting = new Setting();
            firstSetting.BGMValue = INIT_BGM_VALUE;
            firstSetting.SEValue = INIT_SE_VALUE;
            firstSetting.IsHardMode = false;
            firstSetting.LanguageNumber = 0;

            string jsonstr = JsonUtility.ToJson(firstSetting);
            File.WriteAllText(filePath, jsonstr);
        }
    }

    /// <summary>
    /// 設定を保存する
    /// </summary>
    private void SaveSettings()
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;
        setting.IsHardMode = hardModeCheckBox.isOn;
        setting.LanguageNumber = languageSelector.languageNumber;

        // 書き込み
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(setting);

        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log($"セーブ完了：{jsonstr}");
    }

    private Setting LoadSettingData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(filePath);
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Setting>(datastr);
    }
}
