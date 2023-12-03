using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LanguageChanger : MonoBehaviour
{
    [SerializeField, Header("�����ꉻ����e�L�X�g")]
    private List<TextMeshProUGUI> localizeTextObject = new List<TextMeshProUGUI>(2);

    [SerializeField,Header("���{��A�p��A������")]
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