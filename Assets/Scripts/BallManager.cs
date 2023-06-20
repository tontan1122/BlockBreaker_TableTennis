using UnityEngine;

public enum State
{
    INIT,           //�{�[�������i�K
    MOVE_START,     //�����o��
    MOVING,         //���쒆
    DEATH,          //�~�X
    GAMEOVER,       //�Q�[���I�[�o�[
}

public class BallManager : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 5;

    [SerializeField, Header("�o���ʒu")]
    private Vector2 spawnPos;

    [SerializeField, Header("�ŏ��̈ړ�����")]
    private Vector3 startMove = new(1, -1, 0);

    private new Rigidbody2D rigidbody;

    private State currentState = State.INIT;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.velocity = startMove.normalized * moveSpeed;

        SetState(State.INIT);
    }

    void Update()
    {
        switch(currentState)
        {
            case State.INIT:
                Debug.Log("�{�[������");
                Vector2 startPos = spawnPos;
                gameObject.transform.position = startPos;   //�����X�|�[���ʒu�ɍ��W��ݒ�

                SetState(State.MOVE_START);
                break;
            case State.MOVE_START:
                rigidbody.velocity = startMove.normalized * moveSpeed;
                SetState(State.MOVING);
                break;
            case State.MOVING:

                Vector2 currentVelocity = rigidbody.velocity;
                Debug.Log(currentVelocity);
                rigidbody.velocity = currentVelocity.normalized * moveSpeed;

                break;
            case State.DEATH:
                rigidbody.velocity = new Vector2(0, 0);

                SetState(State.INIT);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("��ʊO�ł�");
        if (collision.gameObject.CompareTag("DeathArea")){
            SetState(State.DEATH);
        }
    }
}
