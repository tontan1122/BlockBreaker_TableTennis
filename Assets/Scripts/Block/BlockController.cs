using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField, Header("����܂ł̉�")]
    private int hitPoint = 1;

    [SerializeField, Header("���邩�ǂ���")]
    private bool isBreak = true;

    private SpriteRenderer spriteRenderer;  //�F��ς����肷�邩��

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitPoint--;
            BreakCheak();
        }
    }

    private void BreakCheak()
    {
        if (hitPoint <= 0 && isBreak)
        {
            Destroy(gameObject);
            Debug.Log("����I�I");
        }
    }
}
