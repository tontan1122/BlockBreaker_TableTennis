using UnityEngine;

/// <summary>
/// �J�[�\��������s��
/// </summary>
internal class CursorController : MonoBehaviour
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
    internal void CursorOff()
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
    internal void CursorOn()
    {
        if (isCursorActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
