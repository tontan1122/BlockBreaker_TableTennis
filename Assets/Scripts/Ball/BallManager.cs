using DG.Tweening;
using UnityEngine;

/// <summary>
/// �{�[���̏��
/// </summary>
public enum State
{
    /// <summary> ���ˑO </summary>
    BEFORE_LAUNCH,
    /// <summary> �����o�� </summary>
    MOVE_START,
    /// <summary> ���쒆 </summary>
    MOVING,
    /// <summary> �A�j���[�V������ </summary>
    ANIMATION,
    /// <summary>�@�~�X </summary>
    DEATH,
}

/// <summary>
/// �{�[���̏�ԊǗ�
/// </summary>
public class BallManager : MonoBehaviour
{
    [SerializeField, Header("�N���X�Q��")]
    private BallAudioManager AudioManager;

    private BallController ballController;
    private BallEffectGenerate effectGenerate;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D circleCollider;

    // �{�[���̏����ʒu
    private static readonly float BALL_START_POSITION = 0.5f;

    // �o���ʒu
    private Vector2 spawnPos;

    // ��ԊǗ��ϐ�
    private State currentState = State.BEFORE_LAUNCH;

    // �~�X������
    private int missCount = 0;

    // �����Ă�����
    private bool isMove = false;
    public bool GetIsMove { get { return isMove; } }

    // �ł��Ƃ��ł��邩�ǂ���
    private bool isShot = true;

    // �~�X�������ǂ���
    private bool isMiss = false;

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
                gameObject.transform.position = spawnPos;   // �����X�|�[���ʒu�ɍ��W��ݒ�

                if (isShot) // ���˂��Ă������ǂ���
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
                    circleCollider.enabled = false;  // �X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
                }

                break;
            case State.MOVE_START:
                ballController.StartBallMovement();
                isMove = true;
                SetState(State.MOVING);
                break;
            case State.MOVING:
                // �ړ�����
                ballController.MoveBall();

                // �p�ȏ���
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

    public void SetState(State setState)
    {
        currentState = setState;
    }

    /// <summary>
    /// �n���ꂽ�ʒu�Ƀ{�[�����ړ�
    /// </summary>
    /// <param name="Pos">�v���C���[�̍��W</param>
    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += BALL_START_POSITION;     // �v���C���[�̃o�[����ǂ̂��炢�グ�邩
        spawnPos = Pos;
    }


    /// <summary>
    /// ���̃X�e�[�W�ɐi�ނƂ��ɌĂяo��
    /// </summary>
    public void ResetTheBall()
    {
        ballRigidbody.angularVelocity = 0;
        ballRigidbody.velocity = new Vector2(0, 0);
        circleCollider.enabled = false;  // �X�e�[�W�ړ����ɃX�e�[�W�ƐڐG���Ă��܂�����
        ballController.ProcessMissed();

        isShot = false;     // �X�e�[�W�ړ��O�Ƀ{�[���𔭎˂ł��Ȃ��悤�ɂ��邽��

        SetState(State.BEFORE_LAUNCH);
    }



    /// <summary>
    /// �Q�[�������X�^�[�g�������ɌĂяo��
    /// </summary>
    public void RestartTheBall()
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
    public void BackTitle()
    {
        transform.DOMove(new Vector2(0, GlobalConst.TITLE_POSITION + BALL_START_POSITION), 1.0f)
            .SetEase(Ease.InOutCubic);
    }

    public void StartMove()
    {
        circleCollider.enabled = true;

        SetState(State.MOVE_START);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int effectNumber = 0;
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); // �u���b�N�j��SE
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
            AudioManager.PlayBallSE(0); // �ǔ���SE
        }

        effectGenerate.GenerateEffects(collision, effectNumber);  // Effect����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioManager.PlayBallSE(1); // �u���b�N�j��SE
        }
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
}
