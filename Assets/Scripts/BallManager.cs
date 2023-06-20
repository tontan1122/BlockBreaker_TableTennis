using UnityEngine;

public enum State
{
    INIT,           //ボール生成段階
    MOVE_START,     //動き出し
    MOVING,         //動作中
    DEATH,          //ミス
    GAMEOVER,       //ゲームオーバー
}

public class BallManager : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 5;

    [SerializeField, Header("出現位置")]
    private Vector2 spawnPos;

    [SerializeField, Header("最初の移動方向")]
    private Vector3 startMove = new(1, -1, 0);

    private new Rigidbody2D rigidbody;

    private State currentState = State.INIT;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.velocity = startMove.normalized * moveSpeed;

        SetState(State.INIT);
    }

    void Update()
    {
        switch(currentState)
        {
            case State.INIT:
                Debug.Log("ボール生成");
                Vector2 startPos = spawnPos;
                gameObject.transform.position = startPos;   //初期スポーン位置に座標を設定

                SetState(State.MOVE_START);
                break;
            case State.MOVE_START:
                rigidbody.velocity = startMove.normalized * moveSpeed;
                SetState(State.MOVING);
                break;
            case State.MOVING:

                Vector2 currentVelocity = rigidbody.velocity;
                Debug.Log(currentVelocity);
                rigidbody.velocity = currentVelocity.normalized * moveSpeed;

                break;
            case State.DEATH:
                rigidbody.velocity = new Vector2(0, 0);

                SetState(State.INIT);

                //SetState(State.GAMEOVER);
                break;
            case State.GAMEOVER:
                
                break;
            default:
                
                break;
        }
    }

    private void SetState(State setState)
    {
        currentState = setState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("画面外です");
        if (collision.gameObject.CompareTag("DeathArea")){
            SetState(State.DEATH);
        }
    }
}
