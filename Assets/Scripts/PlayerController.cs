using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ��X�s�[�h")]
    private float moveSpeed = 10.0f;

    [SerializeField, Header("�ړ��͈�:X")]
    private float moveLimitX;

    private Rigidbody2D playerRigidbody;

    private bool isControl = true;      //�������邩�ǂ���

    void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isControl)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // �J�[�\���ʒu�����[���h���W�ɕϊ�
            Vector2 direction = target - (Vector2)transform.position;
            direction.y = 0;

            playerRigidbody.velocity = direction * moveSpeed;

        }
        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        transform.position = currentPos;
    }

    public Vector3 GetPlayerPosition
    {
        get { return transform.position; }
    }

    public bool GetIsControl
    {
        get { return isControl; }
    }

    public void NextStageMove()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, transform.position.y + 15), 1.0f)
            .OnComplete(() => isControl = true);
    }

}
