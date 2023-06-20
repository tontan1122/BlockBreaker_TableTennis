using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ��X�s�[�h")]
    private float moveSpeed = 10.0f;

    [SerializeField, Header("�ړ��͈�:X")]
    private float moveLimitX;

    private new Rigidbody2D rigidbody;


    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector2 direction = target - (Vector2)transform.position;
        direction.y = 0;

        rigidbody.velocity = direction * moveSpeed;

        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        transform.position = currentPos;
    }

}
