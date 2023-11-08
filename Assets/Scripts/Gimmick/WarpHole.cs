using System.Collections;
using UnityEngine;

public class WarpHole : MonoBehaviour
{
    [Header("カラータイプ")]
    [Header("0:None")]
    [Header("1:Red")]
    [Header("2:Blue")]
    [Header("3:Yellow")]
    [SerializeField]
    private int colorType;

    [SerializeField, Header("移動先のワープホール")]
    private WarpHole destinationWarpHole;

    [SerializeField, Header("再びワープできるようになるまでの時間")]
    private float reWarpTime = 3.0f;

    private bool isPossibleWarp = true;    // ワープできるかどうか

    private void Start()
    {
        ChangeHoleColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isPossibleWarp)
        {
            collision.transform.position = destinationWarpHole.transform.position;
            ImpossibleToWarp();
        }
    }

    /// <summary>
    /// ワープをできなくする
    /// </summary>
    internal void ImpossibleToWarp()
    {
        isPossibleWarp = false;
        destinationWarpHole.IsPossibleWarp = false;
        GetComponent<SpriteRenderer>().color = new Color(179, 179, 179);
        destinationWarpHole.GetComponent<SpriteRenderer>().color = new Color(179, 179, 179);
        StartCoroutine(nameof(PossibleToWarp));
    }

    private IEnumerator PossibleToWarp()
    {
        yield return new WaitForSeconds(reWarpTime);
        Debug.Log("ワープできるよ");
        isPossibleWarp = true;
        destinationWarpHole.IsPossibleWarp = true;
        ChangeHoleColor();
        destinationWarpHole.ChangeHoleColor();

    }

    internal bool IsPossibleWarp
    {
        get { return isPossibleWarp; }
        set { isPossibleWarp = value; }
    }

    internal void ChangeHoleColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (colorType)
        {
            case 0:
                break;
            case 1:
                spriteRenderer.color = new Color(255, 0, 0);
                break;
            case 2:
                spriteRenderer.color = new Color(0, 0, 255);
                break;
            case 3:
                spriteRenderer.color = new Color(255, 217, 0);
                break;
            default: break;
        }
    }
}
