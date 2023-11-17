using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WarpAnimation))]
public class WarpHole : MonoBehaviour
{
    [Header("カラータイプ")]
    [SerializeField]
    private ColorType colorType;

    [SerializeField, Header("使用した後の色")]
    private byte usedColor = 150;

    [SerializeField, Header("移動先のワープホール")]
    private WarpHole destinationWarpHole;

    [SerializeField, Header("再びワープできるようになるまでの時間")]
    private float reWarpTime = 3.0f;

    private WarpAnimation warpAnimation;
    private bool isPossibleWarp = true;    // ワープできるかどうか

    private void Start()
    {
        ChangeHoleColor();
        warpAnimation = GetComponent<WarpAnimation>();
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isPossibleWarp)
        {
            isPossibleWarp = false;
            destinationWarpHole.IsPossibleWarp = false;
            BallManager ballManager = collision.gameObject.GetComponent<BallManager>();
            ballManager.SetState(State.ANIMATION);
            await warpAnimation.WarpInAnimationMove(collision.gameObject);
            collision.transform.position = destinationWarpHole.transform.position;
            warpAnimation.WarpOutAnimationMove(collision.gameObject);
            ballManager.SetState(State.MOVING);
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
        GetComponent<SpriteRenderer>().color = new Color32(usedColor, usedColor, usedColor,255);
        destinationWarpHole.GetComponent<SpriteRenderer>().color = new Color32(usedColor, usedColor, usedColor,255);
        StartCoroutine(nameof(PossibleToWarp));
    }

    private IEnumerator PossibleToWarp()
    {
        yield return new WaitForSeconds(reWarpTime);
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
            case ColorType.None:
                break;
            case ColorType.RED:
                spriteRenderer.color = new Color(255, 0, 0);
                break;
            case ColorType.BLUE:
                spriteRenderer.color = new Color(0, 0, 255);
                break;
            case ColorType.YELLOW:
                spriteRenderer.color = new Color(255, 217, 0);
                break;
            default: break;
        }
    }
}

internal enum ColorType
{
    None = 0,
    RED,
    BLUE,
    YELLOW
}
