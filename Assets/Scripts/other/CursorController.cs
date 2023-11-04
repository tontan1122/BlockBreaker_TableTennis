using UnityEngine;

/// <summary>
/// カーソル操作を行う
/// </summary>
internal class CursorController : MonoBehaviour
{
    [SerializeField, Header("カーソルを表示するキャンバス")]
    private Canvas canvas;

    [SerializeField, Header("カーソル画像のRectTransform")]
    private RectTransform cursorTransform;

    [SerializeField, Header("カーソルを消すかどうか")]
    private bool isCursorActive = false;

    void Update()
    {
        CursorPointMove();
        CursorInputAction();
        CursorOff();
    }

    /// <summary>
    /// カーソル移動
    /// </summary>
    private void CursorPointMove()
    {
        // CanvasのRectTransform内にあるマウスの座標をローカル座標に変換する
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
        Input.mousePosition,
            canvas.worldCamera,
            out var mousePosition);

        // ポインターをマウスの座標に移動する
        cursorTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }

    private void CursorInputAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cursorTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            cursorTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    /// <summary>
    /// カーソルの非表示
    /// </summary>
    private void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // 画面内に固定
    }
}
