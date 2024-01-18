using UnityEngine;

/// <summary>
/// �����N���������N���X
/// </summary>
public class InitialBootProcess : MonoBehaviour
{
    [SerializeField,Header("�N���X�Q��")]
    private LanguageSelector languageSelector;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Init"))
        { // "Init"�̃L�[�����݂��Ȃ��ꍇ�͏����N������
            SaveInitialization(); // �Z�[�u�f�[�^��������
            languageSelector.ChangeFromSystemLanguage();
        }
    }

    /// <summary>
    /// ������������������Ԃɂ���
    /// </summary>
    private void SaveInitialization()
    {
        PlayerPrefs.SetInt("Init", 1); // �hInit�h�̃L�[��int�^�̒l(1)�ŕۑ�
    }
}
