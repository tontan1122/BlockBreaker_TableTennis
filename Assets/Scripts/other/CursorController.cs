using UnityEngine;

/// <summary>
/// �J�[�\��������s��
/// </summary>
public class CursorController : MonoBehaviour
{
    [SerializeField, Header("�J�[�\�����������ǂ���")]
    private bool isCursorActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// �J�[�\���̔�\��
    /// </summary>
    public void CursorOff()
    {
        if (isCursorActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined; // ��ʓ��ɌŒ�
        }
    }

    /// <summary>
    /// �J�[�\���̕\��
    /// </summary>
    public void CursorOn()
    {
        if (isCursorActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
