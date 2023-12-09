using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WarpAnimation))]
internal class WarpHole : MonoBehaviour
{
    [Header("�J���[�^�C�v")]
    [SerializeField]
    private ColorType colorType;

    [SerializeField, Header("�g�p������̐F")]
    private byte usedColor = 150;

    [SerializeField, Header("�ړ���̃��[�v�z�[��")]
    private WarpHole destinationWarpHole;

    [SerializeField, Header("�Ăу��[�v�ł���悤�ɂȂ�܂ł̎���")]
    private float reWarpTime = 3.0f;

    private WarpAnimation warpAnimation;
    private OtherAudioManager otherAudioManager;
    private bool isPossibleWarp = true;    // ���[�v�ł��邩�ǂ���

    private void Start()
    {
        ChangeHoleColor();
        warpAnimation = GetComponent<WarpAnimation>();
        otherAudioManager = FindAnyObjectByType<OtherAudioManager>();
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

            await warpAnimation.InitiateWarpAnimation(collision.gameObject);

            collision.transform.position = destinationWarpHole.transform.position;
            warpAnimation.TerminateWarpAnimation(collision.gameObject);
            otherAudioManager.PlaySE(1);
            ballManager.SetState(State.MOVING);
            ImpossibleToWarp();
        }
    }

    /// <summary>
    /// ���[�v���ł��Ȃ�����
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

internal enum ColorType
{
    None = 0,
    GREEN,
    BLUE,
    YELLOW
}
