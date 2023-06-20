using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 5;

    [SerializeField, Header("最初の移動方向")]
    private Vector3 startMove = new(1, -1, 0);

    private new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = startMove.normalized * moveSpeed;
    }

    void Update()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        Debug.Log(currentVelocity);
        rigidbody.velocity = currentVelocity.normalized * moveSpeed;
    }
}
