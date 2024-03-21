using System.Threading;
using System.Collections;
using UnityEngine;

/// <summary>
/// フレームレートの固定化
/// </summary>
public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("フレームレート")]
    private int frameRate = 60;

    private float currentFrameTime;

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // 垂直同期をオフ
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine(nameof(WaitForNextFrame));
    }

    /// <summary>
    /// 次のフレームまで待機するためのコルーチン
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
                Thread.Sleep((int)(sleepTime * 1000));  // 指定時間スレッドの停止
            }
            while (t < currentFrameTime)
            {
                t = Time.realtimeSinceStartup;
            }
        }
    }
}
