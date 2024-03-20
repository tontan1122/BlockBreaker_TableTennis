using UnityEngine;

/// <summary>
/// カーソル操作を行う
/// </summary>
public class CursorController : MonoBehaviour
{
    [SerializeField, Header("カーソルを表示するキャンバス")]
    private Canvas canvas;

    [SerializeField, Header("カーソル画像のRectTransform")]
    private RectTransform cursorTransform;

    private static readonly float CURSOR_SIZE_ON_CLICK = 0.7f;  // クリック時のカーソルサイズ

    void Update()
    {
        MoveCursorPoint();
        DrawingCursorInput();
        HideCursor();
    }

    /// <summary>
    /// カーソル移動
    /// </summary>
    private void MoveCursorPoint()
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

    /// <summary>
    /// カーソルの入力描画
    /// </summary>
    private void DrawingCursorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 画像サイズ変更
            cursorTransform.localScale = new Vector3(CURSOR_SIZE_ON_CLICK, CURSOR_SIZE_ON_CLICK, CURSOR_SIZE_ON_CLICK);
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            cursorTransform.localScale = new Vector3(1, 1, 1);  // 元のサイズに戻す
        }
    }

    /// <summary>
    /// カーソルの非表示
    /// </summary>
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // 画面内に固定
    }
}
