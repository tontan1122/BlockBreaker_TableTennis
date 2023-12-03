using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LanguageChanger : MonoBehaviour
{
    [SerializeField, Header("多言語化するテキスト")]
    private List<TextMeshProUGUI> localizeTextObject = new List<TextMeshProUGUI>(2);

    [SerializeField,Header("日本語、英語、中国語")]
    private LocalizeText[] localizeTexts;

    void Start()
    {
        
    }

    internal void LocalizeText(int language)
    {
        for(int i = 0; i < localizeTextObject.Count; i++)
        {
            localizeTextObject[i].text = localizeTexts[i].text[language];
        }
    }
}

[System.Serializable]
internal class LocalizeText
{
    [Multiline]
    public string[] text;

    internal LocalizeText(string[] texts)
    {
        text = texts;
    }
}