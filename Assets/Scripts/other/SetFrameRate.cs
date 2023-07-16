using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("フレームレート")]
    private int FrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;            //VSync countをDont Syncに変更→垂直同期を無し
        Application.targetFrameRate = FrameRate;   //フレームレートの設定
    }
}
