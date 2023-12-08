using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// その他のゲームSEの再生クラス
/// </summary>
public class OtherAudioManager : MonoBehaviour
{
    [SerializeField, Header("音源")]
    private List<AudioClip> SoundEffects;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="SENumber">
    /// 0：ワープ効果音
    /// </param>
    internal void PlaySE(int SENumber)
    {
        audioSource.PlayOneShot(SoundEffects[SENumber]);
    }
}
