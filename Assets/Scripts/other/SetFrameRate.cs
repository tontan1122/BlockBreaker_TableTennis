using UnityEngine;

/// <summary>
/// �t���[�����[�g�̌Œ艻
/// </summary>
public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("�t���[�����[�g")]
    private int frameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;            // ������������
        Application.targetFrameRate = frameRate;   // �t���[�����[�g�̐ݒ�
    }
}
