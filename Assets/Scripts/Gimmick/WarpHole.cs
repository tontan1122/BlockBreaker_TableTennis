using System.Collections;
using UnityEngine;

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
    private OtherAudioManager otherAudioManager;
    private bool isPossibleWarp = true;    // ワープできるかどうか

    private void Start()
    {
        ChangeHoleColor();
        warpAnimation = new WarpAnimation();
        otherAudioManager = FindAnyObjectByType<OtherAudioManager>();
    }

    private void Update()
    {
        warpAnimation.StartWarpAnimation(gameObject.transform);
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isPossibleWarp)
        {
            isPossibleWarp = false;
            destinationWarpHole.IsPossibleWarp = false;
            otherAudioManager.PlaySE(0);
            BallManager ballManager = collision.gameObject.GetComponent<BallManager>();
            ballManager.SetState(State.ANIMATION);

            await warpAnimation.InitiateWarpAnimation(gameObject.transform.position,collision.gameObject);

            collision.transform.position = destinationWarpHole.transform.position;
            warpAnimation.TerminateWarpAnimation(collision.gameObject);
            otherAudioManager.PlaySE(1);
            ballManager.SetState(State.MOVING);
            DisableWarp();
        }
    }

    /// <summary>
    /// ワープをできなくする
    /// </summary>
    private void DisableWarp()
    {
        isPossibleWarp = false;
        destinationWarpHole.IsPossibleWarp = false;
        GetComponent<SpriteRenderer>().color = new Color32(usedColor, usedColor, usedColor,255);
        destinationWarpHole.GetComponent<SpriteRenderer>().color = new Color32(usedColor, usedColor, usedColor,255);
        StartCoroutine(nameof(AllowWarp));
    }

    /// <summary>
    /// ワープをできるようにする
    /// </summary>
    private IEnumerator AllowWarp()
    {
        yield return new WaitForSeconds(reWarpTime);
        isPossibleWarp = true;
        destinationWarpHole.IsPossibleWarp = true;
        ChangeHoleColor();
        destinationWarpHole.ChangeHoleColor();

    }

    public bool IsPossibleWarp
    {
        get { return isPossibleWarp; }
        set { isPossibleWarp = value; }
    }

    private void ChangeHoleColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (colorType)
        {
            case ColorType.None:
                break;
            case ColorType.GREEN:
                spriteRenderer.color = new Color(0, 255, 0);
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

public enum ColorType
{
    None = 0,
    GREEN,
    BLUE,
    YELLOW
}
