using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("�t���[�����[�g")]
    private int FrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = FrameRate;   //�t���[�����[�g�̐ݒ�
    }
}
