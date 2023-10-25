using UnityEngine;

/// <summary>
/// É{Å[ÉãÇÃãììÆñ{ëÃ
/// </summary>
public class BallController : MonoBehaviour
{
    [SerializeField, Header("É{Å[ÉãÇÃã»Ç™ÇËÇ‚Ç∑Ç≥")]
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
    /// É{Å[ÉãÇÃíºê¸ãììÆ
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void BallMove(float moveSpeed)
    {
        currentVelocity = ballRigidbody.velocity;
        ballRigidbody.velocity = currentVelocity.normalized * moveSpeed;
    }

    /// <summary>
    /// É{Å[ÉãÇÃÉJÅ[ÉuãììÆ
    /// </summary>
    public void CurveBall()
    {
        if (currentVelocity.x >= 0 && currentVelocity.y >= 0)//âEè„
        {
            if (currentVelocity.x >= currentVelocity.y)//âE
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//è„
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime, 0));
                }
            }
        }
        else if (currentVelocity.x > 0 && currentVelocity.y < 0)//âEâ∫
        {
            if (currentVelocity.x >= currentVelocity.y * -1)//âE
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x < currentVelocity.y * -1)//â∫
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y < 0)//ç∂â∫
        {
            if (currentVelocity.x >= currentVelocity.y)//â∫
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(ballRotation.GetRotationSpeed / bendingValue * Time.deltaTime, 0));
                }
            }
            else if (currentVelocity.x < currentVelocity.y)//ç∂
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
            }
        }
        else if (currentVelocity.x < 0 && currentVelocity.y > 0)//ç∂è„
        {
            if (currentVelocity.x * -1 >= currentVelocity.y)//ç∂
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
                else if (ballRotation.GetRotationDirection < 0)
                {
                    ballRigidbody.AddForce(new Vector2(0, ballRotation.GetRotationSpeed / -bendingValue * Time.deltaTime));
                }
            }
            else if (currentVelocity.x * -1 < currentVelocity.y)//è„
            {
                if (ballRotation.GetRotationDirection > 0)//âÒì]ÇÃîªíf
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
