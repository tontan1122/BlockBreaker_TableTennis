using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("�t���[�����[�g")]
    private int FrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;            //VSync count��Dont Sync�ɕύX�����������𖳂�
        Application.targetFrameRate = FrameRate;   //�t���[�����[�g�̐ݒ�
    }
}
