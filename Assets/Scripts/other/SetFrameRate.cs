using UnityEngine;

/// <summary>
/// フレームレートの固定化
/// </summary>
public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("フレームレート")]
    private int frameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;            // 垂直同期あり
        Application.targetFrameRate = frameRate;   // フレームレートの設定
    }
}
