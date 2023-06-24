using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField, Header("壊れるまでの回数")]
    private int hitPoint = 1;

    [SerializeField, Header("壊れるかどうか")]
    private bool isBreak = true;

    private SpriteRenderer spriteRenderer;  //色を変えたりするかも

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
            Debug.Log("壊れる！！");
        }
    }
}
