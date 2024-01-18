using DG.Tweening;
using UnityEngine;

/// <summary>
/// ボールの状態
/// </summary>
public enum State
{
    /// <summary> 発射前 </summary>
    BEFORE_LAUNCH,
    /// <summary> 動き出し </summary>
    MOVE_START,
    /// <summary> 動作中 </summary>
    MOVING,
    /// <summary> アニメーション中 </summary>
    ANIMATION,
    /// <summary>　ミス </summary>
    DEATH,
}

/// <summary>
/// ボールの状態管理
/// </summary>
public class BallManager : MonoBehaviour
{
    [SerializeField, Header("クラス参照")]
    private BallAudioManager AudioManager;

    private BallController ballController;
    private BallEffectGenerate effectGenerate;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    // ボールの初期位置
    private static readonly float BALL_START_POSITION = 0.5f;

    // 出現位置
    private Vector2 spawnPos;

    // 状態管理変数
    private State currentState = State.BEFORE_LAUNCH;

    // ミスした回数
    private int missCount = 0;

    // 動いていいか
    private bool isMove = false;
    public bool GetIsMove { get { return isMove; } }

    // 打つことができるかどうか
    private bool isShot = true;

    // ミスしたかどうか
    private bool isMiss = false;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        ballController = GetComponent<BallController>();
        effectGenerate = GetComponent<BallEffectGenerate>();

        ballController.Initialize(ballRigidbody);
        SetState(State.BEFORE_LAUNCH);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.BEFORE_LAUNCH:
                isMove = false;
                ballRigidbody.angularVelocity = 0;
                gameObject.transform.position = spawnPos;   // 初期スポーン位置に座標を設定

                if (isShot) // 発射していいかどうか
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
                    circleCollider.enabled = false;  // ステージ移動中にステージと接触してしまうため
                }

                break;
            case State.MOVE_START:
                ballController.StartBallMovement();
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                // 移動処理
                ballController.MoveBall();

                // 湾曲処理
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

    public void SetState(State setState)
    {
        currentState = setState;
    }

    /// <summary>
    /// 渡された位置にボールを移動
    /// </summary>
    /// <param name="Pos">プレイヤーの座標</param>
    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += BALL_START_POSITION;     // プレイヤーのバーからどのくらい上げるか
        spawnPos = Pos;
    }


    /// <summary>
    /// 次のステージに進むときに呼び出す
    /// </summary>
    public void ResetTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  // ステージ移動中にステージと接触してしまうため
        ballController.ProcessMissed();

        isShot = false;     // ステージ移動前にボールを発射できないようにするため

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// ゲームをリスタートした時に呼び出す
    /// </summary>
    public void RestartTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPos;
        ballController.ProcessMissed();

        isShot = false;     // ボールを放つ前にステージに当たらないようにするため

        SetState(State.BEFORE_LAUNCH);
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void BackTitle()
    {
        transform.DOMove(new Vector2(0, GlobalConst.TITLE_POSITION + BALL_START_POSITION), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    public void StartMove()
    {
        circleCollider.enabled = true;

        SetState(State.MOVE_START);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int effectNumber = 0;
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); // ブロック破壊SE
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
            AudioManager.PlayBallSE(0); // 壁反射SE
        }

        effectGenerate.GenerateEffects(collision, effectNumber);  // Effect生成
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); // ブロック破壊SE
        }
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
}
