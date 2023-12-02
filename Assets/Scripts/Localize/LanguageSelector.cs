using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal enum LanguageNumber
{
    JAPANESE = 0,
    ENGLISH,
    CHINESE
}

public class LanguageSelector : MonoBehaviour
{
    [SerializeField, Header("����I���{�^��")]
    private List<Button> languageSelectButtons = new List<Button>(3);

    private LanguageChanger languageChanger;
    private int languageNumber = (int)LanguageNumber.JAPANESE;

    void Start()
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

        languageChanger = GetComponent<LanguageChanger>();
        languageChanger.LocalizeText(languageNumber);   // �e�L�X�g�ύX

        languageSelectButtons[0].onClick.AddListener(() => ChangeLanguageButtonPush(0));
        languageSelectButtons[1].onClick.AddListener(() => ChangeLanguageButtonPush(1));
        languageSelectButtons[2].onClick.AddListener(() => ChangeLanguageButtonPush(2));

    }

    /// <summary>
    /// ����ύX�{�^���̃v�b�V����
    /// </summary>
    private void ChangeLanguageButtonPush(int languageNum)
    {
        languageNumber = languageNum;
        languageChanger.LocalizeText(languageNumber);   // �e�L�X�g�ύX
    }
}
