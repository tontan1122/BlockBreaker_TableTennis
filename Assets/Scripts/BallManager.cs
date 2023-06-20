using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 5;

    [SerializeField, Header("�ŏ��̈ړ�����")]
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
