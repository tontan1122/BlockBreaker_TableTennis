using System.Threading;
using System.Collections;
using UnityEngine;

/// <summary>
/// �t���[�����[�g�̌Œ艻
/// </summary>
public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("�t���[�����[�g")]
    private int frameRate = 60;

    private float currentFrameTime;

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // �����������I�t
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine(nameof(WaitForNextFrame));
    }

    /// <summary>
    /// ���̃t���[���܂őҋ@���邽�߂̃R���[�`��
    /// </summary>
    private IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return null;
            currentFrameTime += 1.0f / frameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;

            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime * 1000));  // �w�莞�ԃX���b�h�̒�~
            }
            while (t < currentFrameTime)
            {
                t = Time.realtimeSinceStartup;
            }
        }
    }
}
