using DG.Tweening;
using UnityEngine;

/// <summary>
/// プレイヤーの挙動クラス
/// </summary>
internal class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動スピード")]
    private float moveSpeed = 10.0f;

    [SerializeField, Header("移動範囲:X")]
    private float moveLimitX;

    [SerializeField, Header("カーソルから横にどれだけずらすか")]
    private float displaceX = 0;

    private Rigidbody2D playerRigidbody;

    private bool isControl = true;      //動かせるかどうか

    void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isControl)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // カーソル位置をワールド座標に変換
            Vector2 direction = target - (Vector2)transform.position;
            direction.x += displaceX;   // カーソル位置からずらす
            direction.y = 0;

            playerRigidbody.velocity = direction * moveSpeed;

        }
        /*画面端処理*/
        Vector3 currentPos = transform.position;

        //Mathf.ClampでXの値を最小～最大の範囲内に収める。
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        //端だったときに動く処理を行わないようにする処理
        if(currentPos.x == moveLimitX)
        {
            if(playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector2(0,0);
            }
        }
        else if(currentPos.x == -moveLimitX) 
        {
            if (playerRigidbody.velocity.x < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

        transform.position = currentPos;
    }

    internal Vector3 GetPlayerPosition
    {
        get { return transform.position; }
    }

    internal bool GetIsControl
    {
        get { return isControl; }
    }

    /// <summary>
    /// プレイヤーを次のステージに移動する
    /// </summary>
    internal void NextStageMove()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, transform.position.y + GlobalConst.STAGE_SIZE_Y), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }

    internal void TitlePosMove()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, -18), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }
}
