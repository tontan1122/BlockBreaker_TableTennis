using UnityEngine;

/// <summary>
/// �t���[�����[�g�̌Œ艻
/// </summary>
internal class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("�t���[�����[�g")]
    private int frameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;            //VSync count��Dont Sync�ɕύX�����������𖳂�
        Application.targetFrameRate = frameRate;   //�t���[�����[�g�̐ݒ�
    }
}
