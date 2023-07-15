using UnityEngine;

/// <summary>
/// ゲームのシステムオーディオ
/// </summary>
public class GameAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField, Header("ボタン押すときのSE")]private AudioClip puchButtonSE;
    [SerializeField, Header("ステージ移動のSE")] private AudioClip moveStageSE;
    [SerializeField, Header("ステージクリア時のSE")] private AudioClip StageClearSE;

    public void ClickButtonAudio()
    {
        audioSource.PlayOneShot(puchButtonSE);
    }

    public void MoveStageAudio()
    {
        audioSource.PlayOneShot(moveStageSE);
    }

    public void ClearStageAudio()
    {
        audioSource.PlayOneShot(StageClearSE);
    }
}
