using UnityEngine;

/// <summary>
/// �{�[���̋����{��
/// </summary>
public class BallController : MonoBehaviour
{
    [SerializeField, Header("�{�[���̋Ȃ���₷��")]
    private float bendingValue = 10;

    private Rigidbody2D ballRigidbody;

    private BallRotation ballRotation;

    private Vector2 currentVelocity;



    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        ballRotation = GetComponent<BallRotation>();
    }

    /// <summary>
    /// �{�[���̒�������
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void BallMove(float moveSpeed)
    {
        currentVelocity = ballRigidbody.velocity;
        ballRigidbody.velocity = currentVelocity.normalized * moveSpeed;
    }

    /// <summary>
    /// �{�[���̃J�[�u����
    /// </summary>
    public void CurveBall()
    {
        if (currentVelocity.x >= 0 && currentVelocity.y >= 0)//�E��
        {
            if (currentVelocity.x >= currentVelocity.y)//�E
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
            }
        }
        else if (currentVelocity.x > 0 && currentVelocity.y < 0)//�E��
        {
            if (currentVelocity.x >= currentVelocity.y * -1)//�E
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x < currentVelocity.y * -1)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y < 0)//����
        {
            if (currentVelocity.x >= currentVelocity.y)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y > 0)//����
        {
            if (currentVelocity.x * -1 >= currentVelocity.y)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x * -1 < currentVelocity.y)//��
            {
                if (ballRotation.GetRotationDirection > 0)//��]�̔��f
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
            }
        }
    }
}
