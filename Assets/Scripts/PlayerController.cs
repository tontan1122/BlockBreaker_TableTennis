using DG.Tweening;
using UnityEngine;

/// <summary>
/// プレイヤーの挙動クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動スピード")]
    protected float moveSpeed = 10.0f;

    [SerializeField, Header("移動範囲:X")]
    private float moveLimitX;

    [SerializeField, Header("カーソルから横にどれだけずらすか")]
    private float displaceX = 0;

    protected Rigidbody2D playerRigidbody;

    private bool isControl = true;      //動かせるかどうか

    private void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveThePlayer();   // 移動処理

        LimitPlayerMovement();
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    protected virtual void MoveThePlayer()
    {
        if (isControl)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // カーソル位置をワールド座標に変換
            Vector2 direction = target - (Vector2)transform.position;
            direction.x += displaceX;   // カーソル位置からずらす
            direction.y = 0;

            playerRigidbody.velocity = direction * moveSpeed;
        }
    }

    /// <summary>
    /// プレイヤーの移動制限
    /// </summary>
    protected virtual void LimitPlayerMovement()
    {
        /*画面端処理*/
        Vector3 currentPos = transform.position;

        //Mathf.ClampでXの値を最小～最大の範囲内に収める。
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        //端だったときに動く処理を行わないようにする処理
        if (currentPos.x == moveLimitX)
        {
            if (playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }
        else if (currentPos.x == -moveLimitX)
        {
            if (playerRigidbody.velocity.x < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

        transform.position = currentPos;
    }

    public Vector3 GetPlayerPosition
    {
        get { return transform.position; }
    }

    public bool GetIsControl
    {
        get { return isControl; }
    }

    /// <summary>
    /// プレイヤーを次のステージに移動する
    /// </summary>
    public void MoveNextStage()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, transform.position.y + GlobalConst.STAGE_SIZE_Y), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }

    public void MoveTitlePos()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, GlobalConst.TITLE_POSITION), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }
}
