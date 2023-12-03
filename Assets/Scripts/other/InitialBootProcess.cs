using UnityEngine;

internal class InitialBootProcess : MonoBehaviour
{
    [SerializeField,Header("�N���X�Q��")]
    private LanguageSelector languageSelector;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Init"))
        { // "Init"�̃L�[�����݂��Ȃ��ꍇ�͏����N������
            SaveDataInitialize(); // �Z�[�u�f�[�^��������
            languageSelector.ChangeFromSystemLanguage();
        }
    }
    void SaveDataInitialize()
    {
        PlayerPrefs.SetInt("Init", 1); // �hInit�h�̃L�[��int�^�̒l(1)�ŕۑ�
    }
}
