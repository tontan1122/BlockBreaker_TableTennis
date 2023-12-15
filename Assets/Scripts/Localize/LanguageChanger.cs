using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// ����̕ύX�N���X
/// </summary>
public class LanguageChanger : MonoBehaviour
{
    [SerializeField, Header("�����ꉻ����e�L�X�g")]
    private List<TextMeshProUGUI> localizeTextObjects = new List<TextMeshProUGUI>(2);

    [SerializeField,Header("���{��A�p��A������̕���")]
    private LocalizeText[] localizeTexts;

    [SerializeField, Header("���͂̃t�H���g�T�C�Y")]
    private TextFontSize[] textFontSizes;

    [SerializeField, Header("�g�p����t�H���g�A�Z�b�g")]
    private TextFontAsset[] textFontAssets;

    /// <summary>
    /// ����ύX�A����ɔ����t�H���g�T�C�Y�̕ύX
    /// </summary>
    /// <param name="language">�g�p���錾��</param>
    internal void LocalizeText(int language)
    {
        for(int i = 0; i < localizeTextObjects.Count; i++)
        {
            localizeTextObjects[i].text = localizeTexts[i].text[language];
            localizeTextObjects[i].fontSize = textFontSizes[i].fontSizes[language];
            localizeTextObjects[i].font = textFontAssets[i].fontAssets[language];
        }
    }
}


/// <summary>
/// Inspector�œ񎟌��z���\�����邽�߂̃N���X
/// </summary>
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

/// <summary>
/// Inspector�œ񎟌��z���\�����邽�߂̃N���X
/// </summary>
[System.Serializable]
internal class TextFontSize
{
    public float[] fontSizes;

    internal TextFontSize(float[] size)
    {
        fontSizes = size;
    }
}

/// <summary>
/// Inspector�œ񎟌��z���\�����邽�߂̃N���X
/// </summary>
[System.Serializable]
internal class TextFontAsset
{
    public TMP_FontAsset[] fontAssets;

    internal TextFontAsset(TMP_FontAsset[] size)
    {
        fontAssets = size;
    }
}