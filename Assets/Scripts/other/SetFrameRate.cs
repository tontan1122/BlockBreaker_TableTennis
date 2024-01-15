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
        QualitySettings.vSyncCount = 0;            //VSync countをDont Syncに変更→垂直同期を無し
        Application.targetFrameRate = frameRate;   //フレームレートの設定
    }
}
