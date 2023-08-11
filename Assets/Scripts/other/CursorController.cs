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
    /// カーソルの非表示
    /// </summary>
    public void CursorOff()
    {
#if UNITY_EDITOR
#else
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // 画面内に固定
# endif
    }

    /// <summary>
    /// カーソルの表示
    /// </summary>
    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
