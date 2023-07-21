using UnityEngine;
using TMPro;

public class SmoothBlinkingText : MonoBehaviour
{
    [SerializeField, Header("点滅の間隔（秒）")]
    private float blinkInterval = 1f;
    [SerializeField, Header("フェードの時間（秒）")]
    private float fadeDuration = 0.5f;

    private TextMeshProUGUI text;
    private float timer;
    private bool isFadingOut;
    private float fadeTimer;

    private int clickCount = 0;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = 0f;
        isFadingOut = false;
        fadeTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            text.text = ("Click To Start");
            clickCount++;
        }

        if(clickCount >= 2)
        {
            gameObject.SetActive(false);
        }


        timer += Time.deltaTime;

        // 点滅の間隔ごとに透明度を切り替える
        if (timer >= blinkInterval)
        {
            timer = 0f;
            isFadingOut = !isFadingOut;
            fadeTimer = 0f;
        }

        // フェードアウト中かフェードイン中かに応じて透明度を変化させる
        if (isFadingOut)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
        else
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }

    }

    public void TextDisplay()
    {
        text.text = ("Click To Release");
        clickCount = 0;
        gameObject.SetActive(true);
    }
}
