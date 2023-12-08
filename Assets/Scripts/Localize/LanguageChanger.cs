using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 言語の変更クラス
/// </summary>
public class LanguageChanger : MonoBehaviour
{
    [SerializeField, Header("多言語化するテキスト")]
    private List<TextMeshProUGUI> localizeTextObject = new List<TextMeshProUGUI>(2);

    [SerializeField,Header("日本語、英語、中国語の文章")]
    private LocalizeText[] localizeTexts;

    [SerializeField, Header("文章のフォントサイズ")]
    private TextFontSize[] textFontSize;

    [SerializeField, Header("使用するフォントアセット")]
    private TextFontAsset[] TextFontAsset;

    /// <summary>
    /// 言語変更、それに伴うフォントサイズの変更
    /// </summary>
    /// <param name="language">使用する言語</param>
    internal void LocalizeText(int language)
    {
        for(int i = 0; i < localizeTextObject.Count; i++)
        {
            localizeTextObject[i].text = localizeTexts[i].text[language];
            localizeTextObject[i].fontSize = textFontSize[i].fontSize[language];
            localizeTextObject[i].font = TextFontAsset[i].fontAsset[language];
        }
    }
}


/// <summary>
/// Inspectorで二次元配列を表示するためのクラス
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
/// Inspectorで二次元配列を表示するためのクラス
/// </summary>
[System.Serializable]
internal class TextFontSize
{
    public float[] fontSize;

    internal TextFontSize(float[] size)
    {
        fontSize = size;
    }
}

/// <summary>
/// Inspectorで二次元配列を表示するためのクラス
/// </summary>
[System.Serializable]
internal class TextFontAsset
{
    public TMP_FontAsset[] fontAsset;

    internal TextFontAsset(TMP_FontAsset[] size)
    {
        fontAsset = size;
    }
}