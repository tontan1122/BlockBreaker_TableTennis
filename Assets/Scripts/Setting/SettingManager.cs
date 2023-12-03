using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
internal class Setting
{
    public float BGMValue;
    public float SEValue;
    public bool IsHardMode;
    public int LanguageNumber = 0;
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
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private LanguageSelector languageSelector;

    [SerializeField, Header("BGM�̏����ݒ�")]
    private int firstBGMValue = 60;

    [SerializeField, Header("SE�̏����ݒ�")]
    private int firstSEValue = 75;

    [SerializeField, Header("�`�F�b�N�{�b�N�X")]
    private Toggle hardModeCheckBox;

    private string filePath; // �t�@�C���̃p�X���w��

    private void Awake()
    {
        FileCheck();

        Setting setting = LoadSettingData();

        audioVolume.AudioInit(setting.BGMValue, setting.SEValue);
        hardModeCheckBox.isOn = setting.IsHardMode;
        timeManager.TimeChenger(hardModeCheckBox.isOn);
        languageSelector.ChangeLanguageButtonPush(setting.LanguageNumber);

        hardModeCheckBox.onValueChanged.AddListener(timeManager.TimeChenger);   // ���싅�����[�h�̐ݒ�
        returnButton.onClick.AddListener(SettingAdaptation);
    }

    private void FileCheck()
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
            firstSetting.IsHardMode = false;
            firstSetting.LanguageNumber = 0;

            string jsonstr = JsonUtility.ToJson(firstSetting);
            File.WriteAllText(filePath, jsonstr);
            Debug.Log("�t�@�C���̐����ɐ������܂���");
        }
    }

    internal void SettingAdaptation()     //�����Z�[�u����
    {
        Setting setting = new Setting();

        setting.BGMValue = audioVolume.GetBGMValue;
        setting.SEValue = audioVolume.GetSEValue;
        setting.IsHardMode = hardModeCheckBox.isOn;
        setting.LanguageNumber = languageSelector.GetLanguageNumber;

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
