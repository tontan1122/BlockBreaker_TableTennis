using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールの音管理クラス
/// </summary>
public class BallAudioManager : MonoBehaviour
{
    [SerializeField,Header("音源")]
    private List<AudioClip> ballSoundEffects;

    [SerializeField, Header("音を流すオブジェクト")]
    private AudioSource audioSource;

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
        audioSource.PlayOneShot(ballSoundEffects[SENumber]);
    }
}
