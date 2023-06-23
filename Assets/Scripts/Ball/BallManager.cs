using UnityEngine;

public enum State
{
    BEFORE_LAUNCH,  //���ˑO
    MOVE_START,     //�����o��
    MOVING,         //���쒆
    DEATH,          //�~�X
    GAMEOVER,       //�Q�[���I�[�o�[
}

public class BallManager : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 5;


    [SerializeField, Header("�ŏ��̈ړ�����")]
    private Vector3 startMove = new(1, -1, 0);

    private Rigidbody2D ballRigidbody;

    private Vector2 spawnPos;   //�o���ʒu

    private State currentState = State.BEFORE_LAUNCH;

    private BallController ballController;

    public bool isMove = false;     //�����Ă�����

    private bool isShot = true;     //�ł��Ƃ��ł��邩�ǂ���

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();

        ballController = GetComponent<BallController>();

        SetState(State.BEFORE_LAUNCH);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.BEFORE_LAUNCH:
                isMove = false;
                gameObject.transform.position = spawnPos;   //�����X�|�[���ʒu�ɍ��W��ݒ�
                ballRigidbody.angularVelocity = 0;

                if (isShot) //���˂��Ă������ǂ���
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        SetState(State.MOVE_START);
                    }
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
                ballRigidbody.velocity = new Vector2(0, 0);

                SetState(State.BEFORE_LAUNCH);

                //SetState(State.GAMEOVER);
                break;
            case State.GAMEOVER:

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

    public void SetStartPos(Vector2 Pos)
    {
        Pos.y += 0.5f;     //�v���C���[�̃o�[����ǂ̂��炢�グ�邩
        spawnPos = Pos;
    }

    public bool SetIsShot
    {
        set { isShot = value; }
    }

    public void BallReset()
    {
        SetState(State.BEFORE_LAUNCH);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            Debug.Log("��ʊO�ł�");
            SetState(State.DEATH);
        }
    }
}
