using UnityEngine;
using TMPro;

/// <summary>
/// ブロックオブジェクト
/// </summary>
internal class BlockObject : MonoBehaviour
{
    [SerializeField, Header("壊れるまでの回数")]
    private int hitPoint = 1;

    [SerializeField, Header("壊れるかどうか")]
    private bool isBreak = true;

    private TextMeshPro hitCountText;

    void Start()
    {
        //複数回触れる必要があるなら
        if (hitPoint > 1)
        {
            hitCountText = GetComponentInChildren<TextMeshPro>();
            hitCountText.text = hitPoint.ToString();
        }
        else
        {
            //単発で壊れる
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
            // ブロックの破壊
            Destroy(gameObject);
        }
        else if(isBreak)
        {
            // 残り回数のテキスト変更
            hitCountText.text = hitPoint.ToString();
        }
    }
}
