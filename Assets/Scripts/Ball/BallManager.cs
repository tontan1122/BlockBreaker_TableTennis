using DG.Tweening;
using UnityEngine;

internal enum State
{
    BEFORE_LAUNCH,  //発射前
    MOVE_START,     //動き出し
    MOVING,         //動作中
    ANIMATION,
    DEATH,          //ミス
}

/// <summary>
/// ボールの状態管理
/// </summary>
internal class BallManager : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 5;

    [SerializeField, Header("プレイヤーからどのくらい位置を上げるか")]
    private float ballStartPosition = 0.5f;

    [SerializeField, Header("クラス参照")]
    private BallAudioManager AudioManager;

    private BallController ballController;
    private BallEffectGenerate effectGenerate;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    private static readonly Vector3 INITIAL_DIRECTION = new(0, 1, 0);    // 最初の移動方向

    private Vector2 spawnPos;       //出現位置
    private State currentState = State.BEFORE_LAUNCH;
    private int missCount = 0;      //ミスした回数
    internal bool isMove = false;   // 動いていいか
    private bool isShot = true;     // 打つことができるかどうか
    private bool isMiss = false;    // ミスしたかどうか

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        ballController = GetComponent<BallController>();
        effectGenerate = GetComponent<BallEffectGenerate>();

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
                    if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) && Input.mousePosition.y <= GlobalConst.heightUnavailableClick)
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
                ballRigidbody.velocity = INITIAL_DIRECTION.normalized * moveSpeed;
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                /*移動処理*/
                ballController.BallMove(moveSpeed);

                /*湾曲処理*/
                ballController.CurveBall();

                // 画面外かどうかの確認
                if (gameObject.transform.position.x < spawnPos.x - 20 || gameObject.transform.position.x > spawnPos.x + 20)
                {
                    SetState(State.DEATH);
                }
                if (gameObject.transform.position.y < spawnPos.y - 40 || gameObject.transform.position.y > spawnPos.y + 40)
                {
                    SetState(State.DEATH);
                }
                break;
            case State.ANIMATION:
                ballController.ProcessMissed();
                break;
            case State.DEATH:
                missCount++;
                isMiss = true;
                ResetTheBall();

                break;
            default:

                break;
        }
    }

    internal void SetState(State setState)
    {
        currentState = setState;
    }

    /// <summary>
    /// 渡された位置にボールを移動
    /// </summary>
    /// <param name="Pos">プレイヤーの座標</param>
    internal void SetStartPos(Vector2 Pos)
    {
        Pos.y += ballStartPosition;     //プレイヤーのバーからどのくらい上げるか
        spawnPos = Pos;
    }


    /// <summary>
    /// 次のステージに進むときに呼び出す
    /// </summary>
    internal void ResetTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  //ステージ移動中にステージと接触してしまうため
        ballController.ProcessMissed();

        isShot = false;     //ステージ移動前にボールを発射できないようにするため

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// ゲームをリスタートした時に呼び出す
    /// </summary>
    internal void RestartTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPos;
        ballController.ProcessMissed();

        isShot = false;     // ボールを放つ前にステージに当たらないようにするため

        SetState(State.BEFORE_LAUNCH);
    }

    internal void BackTitle()
    {
        transform.DOMove(new Vector2(0, -18 + ballStartPosition), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int effectNumber = 0;
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); //ブロック破壊SE
        }
        else if (collision.gameObject.CompareTag("DeathArea") || collision.gameObject.CompareTag("DeathBlock"))
        {
            AudioManager.PlayBallSE(2);
            AudioManager.PlayBallSE(3);
            
            // 出すエフェクトを物によって変更
            if (collision.gameObject.CompareTag("DeathBlock"))
            {
                effectNumber = 1;
            }
            else
            {
                effectNumber = 2;
            }
            SetState(State.DEATH);
        }
        else
        {
            AudioManager.PlayBallSE(0); //壁反射SE
        }

        effectGenerate.GenerateEffects(collision, effectNumber);  // Effect生成
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); //ブロック破壊SE
        }
    }

    internal bool SetIsShot
    {
        set { isShot = value; }
    }

    internal float SetMoveSpeed
    {
        set { moveSpeed = value; }
    }

    internal int MissCount
    {
        get { return missCount; }
        set { missCount = value; }
    }

    internal bool IsMiss
    {
        set { isMiss = value; }
        get { return isMiss; }
    }
}
