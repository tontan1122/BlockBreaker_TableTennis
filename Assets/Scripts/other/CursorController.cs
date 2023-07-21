using UnityEngine;

public class CursorController : MonoBehaviour
{
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // ��ʓ��ɌŒ�
    }

    /// <summary>
    /// �J�[�\���̕\��
    /// </summary>
    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
