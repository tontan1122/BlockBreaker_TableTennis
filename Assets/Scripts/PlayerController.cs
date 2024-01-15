using DG.Tweening;
using UnityEngine;

/// <summary>
/// �v���C���[�̋����N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ��X�s�[�h")]
    protected float moveSpeed = 10.0f;

    [SerializeField, Header("�ړ��͈�:X")]
    private float moveLimitX;

    [SerializeField, Header("�J�[�\�����牡�ɂǂꂾ�����炷��")]
    private float displaceX = 0;

    protected Rigidbody2D playerRigidbody;

    private bool isControl = true;      //�������邩�ǂ���

    private void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveThePlayer();   // �ړ�����

        LimitPlayerMovement();
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    protected virtual void MoveThePlayer()
    {
        if (isControl)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);  // �J�[�\���ʒu�����[���h���W�ɕϊ�
            Vector2 direction = target - (Vector2)transform.position;
            direction.x += displaceX;   // �J�[�\���ʒu���炸�炷
            direction.y = 0;

            playerRigidbody.velocity = direction * moveSpeed;
        }
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    protected virtual void LimitPlayerMovement()
    {
        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        //�[�������Ƃ��ɓ����������s��Ȃ��悤�ɂ��鏈��
        if (currentPos.x == moveLimitX)
        {
            if (playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }
        else if (currentPos.x == -moveLimitX)
        {
            if (playerRigidbody.velocity.x < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

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

    /// <summary>
    /// �v���C���[�����̃X�e�[�W�Ɉړ�����
    /// </summary>
    public void MoveNextStage()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, transform.position.y + GlobalConst.STAGE_SIZE_Y), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }

    public void MoveTitlePos()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, GlobalConst.TITLE_POSITION), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }
}
