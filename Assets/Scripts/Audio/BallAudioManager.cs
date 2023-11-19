using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールの音管理クラス
/// </summary>
internal class BallAudioManager : MonoBehaviour
{
    [SerializeField,Header("音源")]
    private List<AudioClip> ballSoundEffects;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="SENumber">
    /// 0：壁反射SE
    /// 1：ブロック破壊SE
    /// 2：ミス時SE
    /// </param>
    internal void PlayBallSE(int SENumber)
    {
        audioSource.PlayOneShot(ballSoundEffects[SENumber]);
    }
}
