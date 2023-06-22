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


    [SerializeField, Header("最初の移動方向")]
    private Vector3 startMove = new(1, -1, 0);

    private new Rigidbody2D rigidbody;
    
    private Vector2 spawnPos;

    private State currentState = State.INIT;

    public bool isMove = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.velocity = startMove.normalized * moveSpeed;

        SetState(State.INIT);
    }

    void Update()
    {
        switch (currentState)
        {
            case State.INIT:
                Debug.Log("ボール生成");
                isMove = false;
                gameObject.transform.position = spawnPos;   //初期スポーン位置に座標を設定
                rigidbody.angularVelocity = 0;

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    SetState(State.MOVE_START);
                }
                break;
            case State.MOVE_START:
                rigidbody.velocity = startMove.normalized * moveSpeed;
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                /*移動処理*/
                Vector2 currentVelocity = rigidbody.velocity;
                rigidbody.velocity = currentVelocity.normalized * moveSpeed;

                DeadPosition();
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

    private void DeadPosition()
    {
        if (gameObject.transform.position.x < spawnPos.x - 20 || gameObject.transform.position.x > spawnPos.x + 20)
        {
            SetState(State.DEATH);
        }
        if (gameObject.transform.position.y < spawnPos.y - 40)
        {
            SetState(State.DEATH);
        }
    }

    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += 0.5f;     //プレイヤーのバーからどのくらい上げるか
        spawnPos = Pos;
    }

    public void BallReset()
    {
        SetState(State.INIT);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            Debug.Log("画面外です");
            SetState(State.DEATH);
        }
    }
}
