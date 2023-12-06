using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal enum LanguageNumber
{
    JAPANESE = 0,
    ENGLISH,
    CHINESE
}

/// <summary>
/// 言語選択を行うクラス
/// </summary>
public class LanguageSelector : MonoBehaviour
{
    [SerializeField, Header("言語選択ボタン")]
    private List<Button> languageSelectButtons = new List<Button>(3);

    private LanguageChanger languageChanger;

    private int languageNumber = (int)LanguageNumber.JAPANESE;  // 言語番号
    internal int GetLanguageNumber { get { return languageNumber; } }

    private void Awake()
    {
        languageChanger = GetComponent<LanguageChanger>();
    }

    void Start()
    {
        languageSelectButtons[0].onClick.AddListener(() => ChangeLanguageButtonPush(0));
        languageSelectButtons[1].onClick.AddListener(() => ChangeLanguageButtonPush(1));
        languageSelectButtons[2].onClick.AddListener(() => ChangeLanguageButtonPush(2));
    }

    /// <summary>
    /// PCの設定されている言語からゲームの言語設定を変更
    /// </summary>
    internal void ChangeFromSystemLanguage()
    {
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            languageNumber = (int)LanguageNumber.JAPANESE;
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            // 英語対応
            languageNumber = (int)LanguageNumber.ENGLISH;
        }
        else if (Application.systemLanguage == SystemLanguage.Chinese)
        {
            languageNumber = (int)LanguageNumber.CHINESE;
        }
        languageChanger.LocalizeText(languageNumber);   // テキスト変更
    }

    /// <summary>
    /// 言語変更ボタンのプッシュ時
    /// </summary>
    internal void ChangeLanguageButtonPush(int languageNum)
    {
        languageNumber = languageNum;
        languageChanger.LocalizeText(languageNumber);   // テキスト変更
    }
}
