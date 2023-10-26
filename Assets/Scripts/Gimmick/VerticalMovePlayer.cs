using UnityEngine;

internal class VerticalMovePlayer : PlayerController
{
    [SerializeField, Header("�ړ��͈́FY")]
    private float moveLimitY;
    protected override void PlayerMove()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector2 direction = target - (Vector2)transform.position;
        direction.x = 0;   // �J�[�\���ʒu���炸�炷
        //direction.y = 0;

        playerRigidbody.velocity = direction * moveSpeed;
        Debug.Log(direction);
    }

    protected override void PlayerMovingLimit()
    {
        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.y = Mathf.Clamp(currentPos.y, -moveLimitY, moveLimitY);

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
