using UnityEngine;

/// <summary>
/// �X�v���C�g�̃t�F�[�h����
/// </summary>
public class FadeSprite : MonoBehaviour
{
    private int fadeSpeed;
    public int SetfadeSpeed { set { fadeSpeed = value; } }

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // �t�F�[�h����
        float spriteAlpha = spriteRenderer.color.a * 255;
        if (spriteAlpha > 0)
        {
            byte fadeNumber = (byte)spriteAlpha;
            fadeNumber -= (byte)fadeSpeed;
            spriteRenderer.color = new Color32(172, 172, 172, fadeNumber);
        }
        else
        {
            return;
        }
    }
}
