using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    BEFORE_LAUNCH,  //発射前
    MOVE_START,     //動き出し
    MOVING,         //動作中
    DEATH,          //ミス
}

[RequireComponent(typeof(AudioSource))]
public class BallManager : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 5;

    [SerializeField, Header("プレイヤーからどのくらい位置を上げるか")]
    private float ballStartPosition = 0.5f;

    [SerializeField, Header("最初の移動方向")]
    private Vector3 startMove = new(0, 1, 0);

    [SerializeField, Header("クラス参照")]
    private BallAudioManager AudioManager;
    private BallController ballController;

    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    private Vector2 spawnPos;   //出現位置

    private State currentState = State.BEFORE_LAUNCH;


    private int missCount = 0;

    public bool isMove = false;     // 動いていいか

    private bool isShot = true;     // 打つことができるかどうか

    private bool isMiss = false;    // ミスしたかどうか

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        ballController = GetComponent<BallController>();

        SetState(State.BEFORE_LAUNCH);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.BEFORE_LAUNCH:
                isMove = false;
                ballRigidbody.angularVelocity = 0;
                gameObject.transform.position = spawnPos;   //初期スポーン位置に座標を設定

                if (isShot) //発射していいかどうか
                {
                    // ボールを放つ時の入力受付
                    if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) && Input.mousePosition.y <= 450)
                    {
                        circleCollider.enabled = true;

                        SetState(State.MOVE_START);
                    }
                }
                else
                {
                    //missCount = 0;  // 移動中はミスカウントを０にする
                    circleCollider.enabled = false;  //ステージ移動中にステージと接触してしまうため
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
                missCount++;
                isMiss = true;
                BallReset();

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

    /// <summary>
    /// 渡された位置にボールを移動
    /// </summary>
    /// <param name="Pos">プレイヤーの座標</param>
    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += ballStartPosition;     //プレイヤーのバーからどのくらい上げるか
        spawnPos = Pos;
    }

    public bool SetIsShot
    {
        set { isShot = value; }
    }

    public int MissCount
    {
        get { return missCount; }
        set { missCount = value; }
    }

    public bool IsMiss
    {
        set { isMiss = value; }
        get { return isMiss; }
    }

    /// <summary>
    /// 次のステージに進むときに呼び出す
    /// </summary>
    public void BallReset()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  //ステージ移動中にステージと接触してしまうため
        gameObject.transform.position = spawnPos;

        isShot = false;     //ステージ移動前にボールを発射できないようにするため

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// ゲームをリスタートした時に呼び出す
    /// </summary>
    public void BallRestart()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPos;

        isShot = false;     // ボールを放つ前にステージに当たらないようにするため

        SetState(State.BEFORE_LAUNCH);
    }

    public void BackTitle()
    {
        transform.DOMove(new Vector2(0, -18 + ballStartPosition), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.HitBlockSound();
        }
        else
        {
            AudioManager.BoundSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            AudioManager.MissSound();
            SetState(State.DEATH);
        }
    }
}
