using UnityEngine;

/// <summary>
/// 初期起動時処理クラス
/// </summary>
public class InitialBootProcess : MonoBehaviour
{
    [SerializeField,Header("クラス参照")]
    private LanguageSelector languageSelector;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Init"))
        { // "Init"のキーが存在しない場合は初期起動処理
            SaveInitialization(); // セーブデータを初期化
            languageSelector.ChangeFromSystemLanguage();
        }
    }

    /// <summary>
    /// 初期化処理をした状態にする
    /// </summary>
    private void SaveInitialization()
    {
        PlayerPrefs.SetInt("Init", 1); // ”Init”のキーをint型の値(1)で保存
    }
}
