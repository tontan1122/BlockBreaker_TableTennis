/// <summary>
/// ボールの音管理クラス
/// </summary>
public class BallAudioManager : AudioManager
{
    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="SENumber">
    /// 0：壁反射SE
    /// 1：ブロック破壊SE
    /// 2：ミス時SE
    /// </param>
    public void PlayBallSE(int SENumber)
    {
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
