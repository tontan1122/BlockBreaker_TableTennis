using System.Collections.Generic;
using UnityEngine;

public class OtherAudioManager : MonoBehaviour
{
    [SerializeField, Header("âπåπ")]
    private List<AudioClip> SoundEffects;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SEÇÃçƒê∂
    /// </summary>
    /// <param name="SENumber">
    /// 0ÅFÉèÅ[Évå¯â âπ
    /// </param>
    internal void PlaySE(int SENumber)
    {
        audioSource.PlayOneShot(SoundEffects[SENumber]);
    }
}
