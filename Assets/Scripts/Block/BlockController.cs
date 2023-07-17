using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    [SerializeField, Header("����܂ł̉�")]
    private int hitPoint = 1;

    [SerializeField, Header("���邩�ǂ���")]
    private bool isBreak = true;

    private TextMeshPro hitCountText;

    void Start()
    {
        //������G���K�v������Ȃ�
        if (hitPoint > 1)
        {
            hitCountText = GetComponentInChildren<TextMeshPro>();
            hitCountText.text = hitPoint.ToString();
        }
        else
        {
            //�P���ŉ���
        }
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
        else if(isBreak)
        {
            hitCountText.text = hitPoint.ToString();
        }
    }
}
