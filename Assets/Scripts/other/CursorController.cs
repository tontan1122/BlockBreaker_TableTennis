using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
