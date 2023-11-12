using UnityEngine;

public class FadeSprite : MonoBehaviour
{
    private int fadeSpeed;
    internal int SetfadeSpeed { set { fadeSpeed = value; } }

    private byte fadeNumber;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float spriteAlpha = spriteRenderer.color.a * 255;
        if (spriteAlpha != 0)
        {
            fadeNumber = (byte)spriteAlpha;
            fadeNumber -= (byte)fadeSpeed;
            spriteRenderer.color = new Color32(172, 172, 172, fadeNumber);
        }
    }
}
