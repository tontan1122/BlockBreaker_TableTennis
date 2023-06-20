using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField, Header("‰ó‚ê‚é‚Ü‚Å‚Ì‰ñ”")]
    private int hitPoint = 1;

    private SpriteRenderer spriteRenderer;  //F‚ğ•Ï‚¦‚½‚è‚·‚é‚©‚à

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

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
        if (hitPoint <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("‰ó‚ê‚éII");
        }
    }
}
