using UnityEngine;

/// <summary>
/// カーソル操作を行う
/// </summary>
public class CursorController : MonoBehaviour
{
    [SerializeField, Header("カーソルを消すかどうか")]
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
    /// カーソルの非表示
    /// </summary>
    public void CursorOff()
    {
        if (isCursorActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined; // 画面内に固定
        }
    }

    /// <summary>
    /// カーソルの表示
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
