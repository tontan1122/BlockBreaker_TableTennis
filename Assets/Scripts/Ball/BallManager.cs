using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    BEFORE_LAUNCH,  //���ˑO
    MOVE_START,     //�����o��
    MOVING,         //���쒆
    DEATH,          //�~�X
}

[RequireComponent(typeof(AudioSource))]
public class BallManager : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 5;

    [SerializeField, Header("�v���C���[����ǂ̂��炢�ʒu���グ�邩")]
    private float ballStartPosition = 0.5f;

    [SerializeField, Header("�ŏ��̈ړ�����")]
    private Vector3 startMove = new(0, 1, 0);

    [SerializeField, Header("�N���X�Q��")]
    private BallAudioManager AudioManager;
    private BallController ballController;

    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    private Vector2 spawnPos;   //�o���ʒu

    private State currentState = State.BEFORE_LAUNCH;


    private int missCount = 0;

    public bool isMove = false;     // �����Ă�����

    private bool isShot = true;     // �ł��Ƃ��ł��邩�ǂ���

    private bool isMiss = false;    // �~�X�������ǂ���

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        ballController = GetComponent<BallController>();

        SetState(State.BEFORE_LAUNCH);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.BEFORE_LAUNCH:
                isMove = false;
                ballRigidbody.angularVelocity = 0;
                gameObject.transform.position = spawnPos;   //�����X�|�[���ʒu�ɍ��W��ݒ�

                if (isShot) //���˂��Ă������ǂ���
                {
                    // �{�[��������̓��͎�t
                    if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) && Input.mousePosition.y <= 450)
                    {
                        circleCollider.enabled = true;

                        SetState(State.MOVE_START);
                    }
                }
                else
                {
                    //missCount = 0;  // �ړ����̓~�X�J�E���g���O�ɂ���
                    circleCollider.enabled = false;  //�X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
                }

                break;
            case State.MOVE_START:
                ballRigidbody.velocity = startMove.normalized * moveSpeed;
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                /*�ړ�����*/
                ballController.BallMove(moveSpeed);

                /*�p�ȏ���*/
                ballController.CurveBall();

                DeadPosition();
                break;
            case State.DEATH:
                missCount++;
                isMiss = true;
                BallReset();

                break;
            default:

                break;
        }
    }

    private void SetState(State setState)
    {
        currentState = setState;
    }

    private void DeadPosition()
    {
        if (gameObject.transform.position.x < spawnPos.x - 20 || gameObject.transform.position.x > spawnPos.x + 20)
        {
            SetState(State.DEATH);
        }
        if (gameObject.transform.position.y < spawnPos.y - 40)
        {
            SetState(State.DEATH);
        }
    }

    /// <summary>
    /// �n���ꂽ�ʒu�Ƀ{�[�����ړ�
    /// </summary>
    /// <param name="Pos">�v���C���[�̍��W</param>
    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += ballStartPosition;     //�v���C���[�̃o�[����ǂ̂��炢�グ�邩
        spawnPos = Pos;
    }

    public bool SetIsShot
    {
        set { isShot = value; }
    }

    public int MissCount
    {
        get { return missCount; }
        set { missCount = value; }
    }

    public bool IsMiss
    {
        set { isMiss = value; }
        get { return isMiss; }
    }

    /// <summary>
    /// ���̃X�e�[�W�ɐi�ނƂ��ɌĂяo��
    /// </summary>
    public void BallReset()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  //�X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
        gameObject.transform.position = spawnPos;

        isShot = false;     //�X�e�[�W�ړ��O�Ƀ{�[���𔭎˂ł��Ȃ��悤�ɂ��邽��

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// �Q�[�������X�^�[�g�������ɌĂяo��
    /// </summary>
    public void BallRestart()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPos;

        isShot = false;     // �{�[������O�ɃX�e�[�W�ɓ�����Ȃ��悤�ɂ��邽��

        SetState(State.BEFORE_LAUNCH);
    }

    public void BackTitle()
    {
        transform.DOMove(new Vector2(0, -18 + ballStartPosition), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.HitBlockSound();
        }
        else
        {
            AudioManager.BoundSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            AudioManager.MissSound();
            SetState(State.DEATH);
        }
    }
}
