using UnityEngine;

/// <summary>
/// ƒ{[ƒ‹‚Ì‹““®–{‘Ì
/// </summary>
internal class BallController : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®‘¬“x")]
    private float moveSpeed = 7;

    private static readonly Vector3 INITIAL_DIRECTION = new(0, 1, 0);    // Å‰‚ÌˆÚ“®•ûŒü
    private static readonly float BENDING_VALUE = 0.025f;   // ‹È‚ª‚è‚â‚·‚³

    private Rigidbody2D ballRigidbody;

    private BallRotation ballRotation;

    private BallAfterImage ballAfterImage;

    private Vector2 currentVelocity;    // Œ»İ‚Ì‰Á‘¬“x

    private void Start()
    {
        ballRotation = new BallRotation(transform);
        ballAfterImage = GetComponent<BallAfterImage>();
    }

    internal void Initialize(Rigidbody2D rigidbody)
    {
        ballRigidbody = rigidbody;
    }

    private void Update()
    {
        ballRotation.HandlingBallRotating(transform);
    }

    internal void StartBallMovement()
    {
        ballRigidbody.velocity = INITIAL_DIRECTION.normalized * moveSpeed;
    }

    /// <summary>
    /// ƒ{[ƒ‹‚Ì’¼ü‹““®
    /// </summary>
    internal void MoveBall()
    {
        currentVelocity = ballRigidbody.velocity;
        ballRigidbody.velocity = currentVelocity.normalized * moveSpeed;

        ballAfterImage.DrawAfterImage(this.transform);
    }

    /// <summary>
    /// ƒ{[ƒ‹‚ÌƒJ[ƒu‹““®
    /// </summary>
    internal void CurveBall()
    {
        if (currentVelocity.x >= 0 && currentVelocity.y >= 0)//‰Eã
        {
            if (currentVelocity.x >= currentVelocity.y)//‰E
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.up);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.up);
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//ã
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.left);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.left);
                }
            }
        }
        else if (currentVelocity.x > 0 && currentVelocity.y < 0)//‰E‰º
        {
            if (currentVelocity.x >= currentVelocity.y * -1)//‰E
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.up);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.up);
                }
            }
            else if (currentVelocity.x < currentVelocity.y * -1)//‰º
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.right);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.right);
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y < 0)//¶‰º
        {
            if (currentVelocity.x >= currentVelocity.y)//‰º
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.right);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.right);
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//¶
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.down);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.down);
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y > 0)//¶ã
        {
            if (currentVelocity.x * -1 >= currentVelocity.y)//¶
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
                {
                    AddForce(Vector2.down);
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    AddForce(Vector2.down);
                }
            }
            else if (currentVelocity.x * -1 < currentVelocity.y)//ã
            {
                if (ballRotation.GetRotationDirection > 0)//‰ñ“]‚Ì”»’f
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

    private void AddForce(Vector2 direction)
    {
        ballRigidbody.AddForce
            (new Vector2(ballRotation.GetRotationSpeed / BENDING_VALUE * Time.deltaTime * direction.x,
            ballRotation.GetRotationSpeed / BENDING_VALUE * Time.deltaTime * direction.y));
    }

    /// <summary>
    /// ƒ~ƒX‚Ìˆ—
    /// </summary>
    internal void ProcessMissed()
    {
        ballAfterImage.RemoveAllAfterImage();
    }
}
