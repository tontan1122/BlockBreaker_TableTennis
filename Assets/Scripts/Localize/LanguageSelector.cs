using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LanguageNumber
{
    JAPANESE = 0,
    ENGLISH,
    CHINESE
}

/// <summary>
/// ����I�����s���N���X
/// </summary>
public class LanguageSelector : MonoBehaviour
{
    [SerializeField, Header("����I���{�^��")]
    private List<Button> languageSelectButtons = new List<Button>(3);

    private LanguageChanger languageChanger;

    public int languageNumber { get; private set; }

    private void Awake()
    {
        languageNumber = (int)LanguageNumber.JAPANESE;
        languageChanger = GetComponent<LanguageChanger>();
    }

    void Start()
    {
        languageSelectButtons[0].onClick.AddListener(() => PushChangeLanguageButton(0));
        languageSelectButtons[1].onClick.AddListener(() => PushChangeLanguageButton(1));
        languageSelectButtons[2].onClick.AddListener(() => PushChangeLanguageButton(2));
    }

    /// <summary>
    /// PC�̐ݒ肳��Ă��錾�ꂩ��Q�[���̌���ݒ��ύX
    /// </summary>
    public void ChangeFromSystemLanguage()
    {
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            languageNumber = (int)LanguageNumber.JAPANESE;
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            // �p��Ή�
            languageNumber = (int)LanguageNumber.ENGLISH;
        }
        else if (Application.systemLanguage == SystemLanguage.Chinese)
        {
            languageNumber = (int)LanguageNumber.CHINESE;
        }
        languageChanger.LocalizeText(languageNumber);   // �e�L�X�g�ύX
    }

    /// <summary>
    /// ����ύX�{�^���̃v�b�V����
    /// </summary>
    public void PushChangeLanguageButton(int languageNum)
    {
        languageNumber = languageNum;
        languageChanger.LocalizeText(languageNumber);   // �e�L�X�g�ύX
    }
}
