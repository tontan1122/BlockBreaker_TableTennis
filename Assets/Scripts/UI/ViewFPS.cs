using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FPS�̕\��
/// </summary>
internal class ViewFPS : MonoBehaviour
{
    [SerializeField]
    private float Interval = 0.1f;

    private Text text;

    private float timeCount;
    private int frames;
    private float time;
    private float fps;

    private void Start()
    {

        // �e�L�X�g�R���|�[�l���g�̎擾
        text = this.GetComponent<Text>();
    }

    // FPS�̕\���ƌv�Z
    private void Update()
    {
        time -= Time.deltaTime;
        timeCount += Time.timeScale / Time.deltaTime;
        frames++;

        if (0 < time) return;

        fps = timeCount / frames;
        time = Interval;
        timeCount = 0;
        frames = 0;

        text.text = "FPS: " + fps.ToString("f2");
    }
}
