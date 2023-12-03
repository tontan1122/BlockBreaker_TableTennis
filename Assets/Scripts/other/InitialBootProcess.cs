using UnityEngine;

internal class InitialBootProcess : MonoBehaviour
{
    [SerializeField,Header("クラス参照")]
    private LanguageSelector languageSelector;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Init"))
        { // "Init"のキーが存在しない場合は初期起動処理
            SaveDataInitialize(); // セーブデータを初期化
            languageSelector.ChangeFromSystemLanguage();
        }
    }
    void SaveDataInitialize()
    {
        PlayerPrefs.SetInt("Init", 1); // ”Init”のキーをint型の値(1)で保存
    }
}
