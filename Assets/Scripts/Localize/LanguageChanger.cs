using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 言語の変更クラス
/// </summary>
public class LanguageChanger : MonoBehaviour
{
    [SerializeField, Header("多言語化するテキスト")]
    private List<TextMeshProUGUI> localizeTextObjects = new List<TextMeshProUGUI>(2);

    [SerializeField,Header("日本語、英語、中国語の文章")]
    private LocalizeText[] localizeTexts;

    [SerializeField, Header("文章のフォントサイズ")]
    private TextFontSize[] textFontSizes;

    [SerializeField, Header("使用するフォントアセット")]
    private TextFontAsset[] textFontAssets;

    /// <summary>
    /// 言語変更、それに伴うフォントサイズの変更
    /// </summary>
    /// <param name="language">使用する言語</param>
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
    public float[] fontSizes;

    internal TextFontSize(float[] size)
    {
        fontSizes = size;
    }
}

/// <summary>
/// Inspectorで二次元配列を表示するためのクラス
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