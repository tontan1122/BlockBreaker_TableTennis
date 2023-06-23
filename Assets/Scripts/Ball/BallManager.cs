using UnityEngine;

public enum State
{
    BEFORE_LAUNCH,  //発射前
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

    private Rigidbody2D ballRigidbody;

    private Vector2 spawnPos;   //出現位置

    private State currentState = State.BEFORE_LAUNCH;

    private BallController ballController;

    public bool isMove = false;     //動いていいか

    private bool isShot = true;     //打つことができるかどうか

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();

        ballController = GetComponent<BallController>();

        SetState(State.BEFORE_LAUNCH);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.BEFORE_LAUNCH:
                isMove = false;
                gameObject.transform.position = spawnPos;   //初期スポーン位置に座標を設定
                ballRigidbody.angularVelocity = 0;

                if (isShot) //発射していいかどうか
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        SetState(State.MOVE_START);
                    }
                }

                break;
            case State.MOVE_START:
                ballRigidbody.velocity = startMove.normalized * moveSpeed;
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                /*移動処理*/
                ballController.BallMove(moveSpeed);
                
                /*湾曲処理*/
                ballController.CurveBall();

                DeadPosition();
                break;
            case State.DEATH:
                ballRigidbody.velocity = new Vector2(0, 0);

                SetState(State.BEFORE_LAUNCH);

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

    public bool SetIsShot
    {
        set { isShot = value; }
    }

    public void BallReset()
    {
        SetState(State.BEFORE_LAUNCH);
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
