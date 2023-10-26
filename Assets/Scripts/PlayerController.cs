using DG.Tweening;
using UnityEngine;

/// <summary>
/// �v���C���[�̋����N���X
/// </summary>
internal class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ��X�s�[�h")]
    private float moveSpeed = 10.0f;

    [SerializeField, Header("�ړ��͈�:X")]
    private float moveLimitX;

    [SerializeField, Header("�J�[�\�����牡�ɂǂꂾ�����炷��")]
    private float displaceX = 0;

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
            direction.x += displaceX;   // �J�[�\���ʒu���炸�炷
            direction.y = 0;

            playerRigidbody.velocity = direction * moveSpeed;

        }
        /*��ʒ[����*/
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X�̒l���ŏ��`�ő�͈͓̔��Ɏ��߂�B
        currentPos.x = Mathf.Clamp(currentPos.x, -moveLimitX, moveLimitX);

        //�[�������Ƃ��ɓ����������s��Ȃ��悤�ɂ��鏈��
        if(currentPos.x == moveLimitX)
        {
            if(playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector2(0,0);
            }
        }
        else if(currentPos.x == -moveLimitX) 
        {
            if (playerRigidbody.velocity.x < 0)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
            }
        }

        transform.position = currentPos;
    }

    internal Vector3 GetPlayerPosition
    {
        get { return transform.position; }
    }

    internal bool GetIsControl
    {
        get { return isControl; }
    }

    /// <summary>
    /// �v���C���[�����̃X�e�[�W�Ɉړ�����
    /// </summary>
    internal void NextStageMove()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, transform.position.y + GlobalConst.STAGE_SIZE_Y), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }

    internal void TitlePosMove()
    {
        isControl = false;
        transform.DOMove(new Vector2(0, -18), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => isControl = true);
    }
}
