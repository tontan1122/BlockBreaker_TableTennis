using UnityEngine;

/// <summary>
/// 縦に動くプレイヤークラス
/// </summary>
public class VerticalMovePlayer : PlayerController
{
    [SerializeField, Header("移動範囲：Y")]
    private float moveLimitY;

    [SerializeField,Header("カーソルからどれだけずらすか:Y")]
    private float displaceY = 0;

    private float firstPosY = 0;

    private void Start()
    {
        firstPosY = this.gameObject.transform.position.y;
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void MoveThePlayer()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // カーソル位置をワールド座標に変換
        Vector2 direction = target - (Vector2)transform.position;
        direction.x = 0;   // カーソル位置からずらす
        direction.y += displaceY;

        playerRigidbody.velocity = direction * moveSpeed;
    }

    protected override void LimitPlayerMovement()
    {
        /*画面端処理*/
        Vector3 currentPos = transform.position;

        //Mathf.ClampでXの値を最小～最大の範囲内に収める。
        currentPos.y = Mathf.Clamp(currentPos.y, -moveLimitY + firstPosY, moveLimitY + firstPosY);

        //端だったときに動く処理を行わないようにする処理
        if (currentPos.y == moveLimitY)
        {
            if (playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }
        else if (currentPos.y == -moveLimitY)
        {
            if (playerRigidbody.velocity.y < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

        transform.position = currentPos;
    }
}
