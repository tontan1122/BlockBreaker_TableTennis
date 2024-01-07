using UnityEngine;

/// <summary>
/// �c�ɓ����v���C���[�N���X
/// </summary>
public class VerticalMovePlayer : PlayerController
{
    [SerializeField, Header("�ړ��͈́FY")]
    private float moveLimitY;

    [SerializeField,Header("�J�[�\������ǂꂾ�����炷��:Y")]
    private float displaceY = 0;

    private float firstPosY = 0;

    private void Start()
    {
        firstPosY = this.gameObject.transform.position.y;
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void MoveThePlayer()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector2 direction = target - (Vector2)transform.position;
        direction.x = 0;   // �J�[�\���ʒu���炸�炷
        direction.y += displaceY;

        playerRigidbody.velocity = direction * moveSpeed;
    }

    protected override void LimitPlayerMovement()
    {
        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.y = Mathf.Clamp(currentPos.y, -moveLimitY + firstPosY, moveLimitY + firstPosY);

        //�[�������Ƃ��ɓ����������s��Ȃ��悤�ɂ��鏈��
        if (currentPos.y == moveLimitY)
        {
            if (playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }
        else if (currentPos.y == -moveLimitY)
        {
            if (playerRigidbody.velocity.y < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

        transform.position = currentPos;
    }
}
