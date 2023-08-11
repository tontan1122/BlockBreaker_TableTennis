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
#if UNITY_EDITOR
#else
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // ��ʓ��ɌŒ�
# endif
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
