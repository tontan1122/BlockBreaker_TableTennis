using UnityEngine;

/// <summary>
/// ボールの挙動本体
/// </summary>
public class BallController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 7;

    // 最初の移動方向
    private static readonly Vector3 INITIAL_DIRECTION = new(0, 1, 0);
    // 曲がりやすさ
    private static readonly float BENDING_VALUE = 0.025f;

    private Rigidbody2D ballRigidbody;

    private BallRotation ballRotation;

    private BallAfterImage ballAfterImage;

    // 現在の加速度
    private Vector2 currentVelocity;

    private void Start()
    {
        ballRotation = new BallRotation(transform);
        ballAfterImage = GetComponent<BallAfterImage>();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="rigidbody">Ballのrigidbody</param>
    public void Initialize(Rigidbody2D rigidbody)
    {
        ballRigidbody = rigidbody;
    }

    private void Update()
    {
        ballRotation.HandlingBallRotating(transform);
    }

    /// <summary>
    /// ボールの発射
    /// </summary>
    public void StartBallMovement()
    {
        ballRigidbody.velocity = INITIAL_DIRECTION.normalized * moveSpeed;
    }

    /// <summary>
    /// ボールの直線挙動
    /// </summary>
    public void MoveBall()
    {
        currentVelocity = ballRigidbody.velocity;
        ballRigidbody.velocity = currentVelocity.normalized * moveSpeed;

        ballAfterImage.DrawAfterImage(this.transform);
    }

    /// <summary>
    /// ボールのカーブ挙動
    /// </summary>
    public void CurveBall()
    {
        if (currentVelocity.x >= 0 && currentVelocity.y >= 0)// 右上
        {
            if (currentVelocity.x >= currentVelocity.y)// 右
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.up);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.up);
                }
            }
            else if (currentVelocity.x < currentVelocity.y)// 上
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.left);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.left);
                }
            }
        }
        else if (currentVelocity.x > 0 && currentVelocity.y < 0)// 右下
        {
            if (currentVelocity.x >= currentVelocity.y * -1)// 右
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.up);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.up);
                }
            }
            else if (currentVelocity.x < currentVelocity.y * -1)// 下
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.right);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.right);
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y < 0)// 左下
        {
            if (currentVelocity.x >= currentVelocity.y)// 下
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.right);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.right);
                }
            }
            else if (currentVelocity.x < currentVelocity.y)// 左
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.down);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.down);
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y > 0)// 左上
        {
            if (currentVelocity.x * -1 >= currentVelocity.y)// 左
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.down);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.down);
                }
            }
            else if (currentVelocity.x * -1 < currentVelocity.y)// 上
            {
                if (ballRotation.GetRotationDirection > 0)// 回転の判断
                {
                    AddForce(Vector2.left);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.left);
                }
            }
        }
    }

    /// <summary>
    /// ボールを曲げるために力を加える
    /// </summary>
    /// <param name="direction">力の方向</param>
    private void AddForce(Vector2 direction)
    {
        ballRigidbody.AddForce
            (new Vector2(ballRotation.GetRotationSpeed / BENDING_VALUE * Time.deltaTime * direction.x,
            ballRotation.GetRotationSpeed / BENDING_VALUE * Time.deltaTime * direction.y));
    }

    /// <summary>
    /// ミス時の処理
    /// </summary>
    public void ProcessMissed()
    {
        ballAfterImage.RemoveAllAfterImage();
    }
}
