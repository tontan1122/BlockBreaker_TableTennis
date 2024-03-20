using UnityEngine;
using TMPro;

/// <summary>
/// �e�L�X�g�̃t�F�[�h
/// </summary>
public class SmoothBlinkingText : MonoBehaviour
{
    [SerializeField, Header("�_�ł̊Ԋu�i�b�j")]
    private float blinkInterval = 1f;
    [SerializeField, Header("�t�F�[�h�̎��ԁi�b�j")]
    private float fadeDuration = 0.5f;

    private TextMeshProUGUI text;
    private float timer;
    private bool isFadingOut;
    private float fadeTimer;

    private int clickCount = 0;

    private bool isSettingActive = false;   // �ݒ��ʂ��\������Ă��邩�ǂ���
    private bool isQuitActive = false;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = 0f;
        isFadingOut = false;
        fadeTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSettingActive && !isQuitActive && Input.mousePosition.y <= GlobalConst.heightUnavailableClick)
        {
            text.text = ("Click To Start");
            clickCount++;
        }

        if(clickCount >= 2)
        {
            gameObject.SetActive(false);
        }


        timer += Time.deltaTime;

        // �_�ł̊Ԋu���Ƃɓ����x��؂�ւ���
        if (timer >= blinkInterval)
        {
            timer = 0f;
            isFadingOut = !isFadingOut;
            fadeTimer = 0f;
        }

        // �t�F�[�h�A�E�g�����t�F�[�h�C�������ɉ����ē����x��ω�������
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

    /// <summary>
    /// �e�L�X�g�̕\��
    /// </summary>
    public void DisplayText()
    {
        text.text = ("Click To Release");
        clickCount = 0;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �\������e�L�X�g�̕ύX
    /// </summary>
    public void ChangeText()
    {
        text.text = ("Click To Start");
        clickCount++;
    }

    /// <summary>
    /// �ݒ��ʂ��o�Ă��邩�ǂ�����ύX����
    /// </summary>
    /// <param name="active">�o�Ă��邩�ǂ���</param>
    public void SetSettingActive(bool active)
    {
        isSettingActive = active;
    }

    /// <summary>
    /// �I����ʂ��o�Ă��邩�ǂ�����ύX����
    /// </summary>
    /// <param name="active">�o�Ă��邩�ǂ���</param>
    public void SetQuitActive(bool active)
    {
        isQuitActive = active;
    }
}
