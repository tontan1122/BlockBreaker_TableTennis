using UnityEngine;
using TMPro;

/// <summary>
/// �u���b�N�I�u�W�F�N�g
/// </summary>
internal class BlockObject : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            hitPoint--;
            BreakCheak();
        }
    }

    private void BreakCheak()
    {
        if (hitPoint <= 0 && isBreak)
        {
            // �u���b�N�̔j��
            Destroy(gameObject);
        }
        else if(isBreak)
        {
            // �c��񐔂̃e�L�X�g�ύX
            hitCountText.text = hitPoint.ToString();
        }
    }
}
