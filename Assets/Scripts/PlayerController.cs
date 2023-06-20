using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動スピード")]
    private float moveSpeed = 10.0f;

    [SerializeField, Header("移動範囲:X")]
    private float moveLimitX;

    private new Rigidbody2D rigidbody;


    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // カーソル位置をワールド座標に変換
        Vector2 direction = target - (Vector2)transform.position;
        direction.y = 0;

        rigidbody.velocity = direction * moveSpeed;

        /*画面端処理*/
        Vector3 currentPos = transform.position;

        //Mathf.ClampでXの値を最小～最大の範囲内に収める。
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        transform.position = currentPos;
    }

}
