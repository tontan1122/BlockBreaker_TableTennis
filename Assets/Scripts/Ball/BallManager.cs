using DG.Tweening;
using UnityEngine;

internal enum State
{
    BEFORE_LAUNCH,  //���ˑO
    MOVE_START,     //�����o��
    MOVING,         //���쒆
    ANIMATION,      //�A�j���[�V������
    DEATH,          //�~�X
}

/// <summary>
/// �{�[���̏�ԊǗ�
/// </summary>
internal class BallManager : MonoBehaviour
{

    [SerializeField, Header("�N���X�Q��")]
    private BallAudioManager AudioManager;

    private BallController ballController;
    private BallEffectGenerate effectGenerate;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    private static readonly float BALL_START_POSITION = 0.4f;   // �{�[���̏����ʒu

    private Vector2 spawnPos;       //�o���ʒu
    private State currentState = State.BEFORE_LAUNCH;
    private int missCount = 0;      //�~�X������
    private bool isMove = false;   // �����Ă�����
    internal bool GetIsMove { get { return isMove; } }

    private bool isShot = true;     // �ł��Ƃ��ł��邩�ǂ���
    private bool isMiss = false;    // �~�X�������ǂ���

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        ballController = GetComponent<BallController>();
        effectGenerate = GetComponent<BallEffectGenerate>();

        ballController.Initialize(ballRigidbody);
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
                    if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) && Input.mousePosition.y <= GlobalConst.heightUnavailableClick)
                    {
                        circleCollider.enabled = true;

                        SetState(State.MOVE_START);
                    }
                }
                else
                {
                    circleCollider.enabled = false;  //�X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
                }

                break;
            case State.MOVE_START:
                ballController.StartBallMovement();
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                /*�ړ�����*/
                ballController.MoveBall();

                /*�p�ȏ���*/
                ballController.CurveBall();

                // ��ʊO���ǂ����̊m�F
                if (gameObject.transform.position.x < spawnPos.x - 20 || gameObject.transform.position.x > spawnPos.x + 20)
                {
                    SetState(State.DEATH);
                }
                if (gameObject.transform.position.y < spawnPos.y - 40 || gameObject.transform.position.y > spawnPos.y + 40)
                {
                    SetState(State.DEATH);
                }
                break;
            case State.ANIMATION:
                ballController.ProcessMissed();
                break;
            case State.DEATH:
                missCount++;
                isMiss = true;
                ResetTheBall();

                break;
            default:

                break;
        }
    }

    internal void SetState(State setState)
    {
        currentState = setState;
    }

    /// <summary>
    /// �n���ꂽ�ʒu�Ƀ{�[�����ړ�
    /// </summary>
    /// <param name="Pos">�v���C���[�̍��W</param>
    internal void SetStartPos(Vector2 Pos)
    {
        Pos.y += BALL_START_POSITION;     //�v���C���[�̃o�[����ǂ̂��炢�グ�邩
        spawnPos = Pos;
    }


    /// <summary>
    /// ���̃X�e�[�W�ɐi�ނƂ��ɌĂяo��
    /// </summary>
    internal void ResetTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  //�X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
        ballController.ProcessMissed();

        isShot = false;     //�X�e�[�W�ړ��O�Ƀ{�[���𔭎˂ł��Ȃ��悤�ɂ��邽��

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// �Q�[�������X�^�[�g�������ɌĂяo��
    /// </summary>
    internal void RestartTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPos;
        ballController.ProcessMissed();

        isShot = false;     // �{�[������O�ɃX�e�[�W�ɓ�����Ȃ��悤�ɂ��邽��

        SetState(State.BEFORE_LAUNCH);
    }

    /// <summary>
    /// �^�C�g���ɖ߂�
    /// </summary>
    internal void BackTitle()
    {
        transform.DOMove(new Vector2(0, GlobalConst.TITLE_POSITION + BALL_START_POSITION), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    internal void StartMove()
    {
        circleCollider.enabled = true;

        SetState(State.MOVE_START);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int effectNumber = 0;
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); //�u���b�N�j��SE
        }
        else if (collision.gameObject.CompareTag("DeathArea") || collision.gameObject.CompareTag("DeathBlock"))
        {
            AudioManager.PlayBallSE(2);
            AudioManager.PlayBallSE(3);
            
            // �o���G�t�F�N�g�𕨂ɂ���ĕύX
            if (collision.gameObject.CompareTag("DeathBlock"))
            {
                effectNumber = 1;
            }
            else
            {
                effectNumber = 2;
            }
            SetState(State.DEATH);
        }
        else
        {
            AudioManager.PlayBallSE(0); //�ǔ���SE
        }

        effectGenerate.GenerateEffects(collision, effectNumber);  // Effect����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); //�u���b�N�j��SE
        }
    }

    internal bool SetIsShot
    {
        set { isShot = value; }
    }

    internal int MissCount
    {
        get { return missCount; }
        set { missCount = value; }
    }

    internal bool IsMiss
    {
        set { isMiss = value; }
        get { return isMiss; }
    }
}
