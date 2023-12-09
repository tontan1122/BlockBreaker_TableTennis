using UnityEngine;

/// <summary>
/// ƒ{[ƒ‹‚Ì‹““®–{‘Ì
/// </summary>
internal class BallController : MonoBehaviour
{
    [SerializeField, Header("ƒ{[ƒ‹‚Ì‹È‚ª‚è‚â‚·‚³")]
    private float bendingValue = 10;

    private Rigidbody2D ballRigidbody;

    private BallRotation ballRotation;

    private BallAfterImage ballAfterImage;

    private Vector2 currentVelocity;    // Œ»İ‚Ì‰Á‘¬“x



    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        ballRotation = GetComponent<BallRotation>();
        ballAfterImage = GetComponent<BallAfterImage>();
    }

    /// <summary>
    /// ƒ{[ƒ‹‚Ì’¼ü‹““®
    /// </summary>
    /// <param name="moveSpeed"></param>
    internal void BallMove(float moveSpeed)
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
            (new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime * direction.x,
            ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime * direction.y));
    }

    /// <summary>
    /// ƒ~ƒX‚Ìˆ—
    /// </summary>
    internal void ProcessMissed()
    {
        ballAfterImage.RemoveAllAfterImage();
    }
}
