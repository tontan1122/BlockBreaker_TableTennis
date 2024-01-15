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
/// �ݒ�֘A�̂��̂��Ǘ�����N���X
/// </summary>
public class SettingManager : MonoBehaviour
{ 
    [SerializeField, Header("�ݒ��ʂ����{�^��")]
    private Button returnButton;

    [SerializeField, Header("�N���X�Q�ƌn")]
    private AudioVolume audioVolume;
    [SerializeField]
    private LanguageSelector languageSelector;

    [SerializeField, Header("�`�F�b�N�{�b�N�X")]
    private Toggle hardModeCheckBox;

    private string filePath; // �t�@�C���̃p�X���w��
    private TimeManager timeManager;

    private static readonly int INIT_BGM_VALUE = 65;  // �N����BGM�̉���
    private static readonly int INIT_SE_VALUE = 70;  // �N����SE�̉���

    private void Awake()
    {
        CheckingFiles();

        Setting setting = LoadSettingData();

        timeManager = new TimeManager();

        audioVolume.InitialAudioSettings(setting.BGMValue, setting.SEValue);
        hardModeCheckBox.isOn = setting.IsHardMode;
        timeManager.ChengeTimeScale(hardModeCheckBox.isOn);
        languageSelector.PushChangeLanguageButton(setting.LanguageNumber);

        hardModeCheckBox.onValueChanged.AddListener(timeManager.ChengeTimeScale);   // ���싅�����[�h�̐ݒ�
        returnButton.onClick.AddListener(SaveSettings);
    }

    /// <summary>
    /// �t�@�C���̊m�F
    /// </summary>
    private void CheckingFiles()
    {
        filePath = Application.persistentDataPath + "/settingdata.json";
        if (File.Exists(filePath))  // JSON�t�@�C�������݂��邩�ǂ���
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
    /// �ݒ��ۑ�����
    /// </summary>
    private void SaveSettings()
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;
        setting.IsHardMode = hardModeCheckBox.isOn;
        setting.LanguageNumber = languageSelector.languageNumber;

        // ��������
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(setting);

        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log($"�Z�[�u�����F{jsonstr}");
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
