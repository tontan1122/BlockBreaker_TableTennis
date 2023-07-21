using UnityEngine;

/// <summary>
/// ボールの音管理クラス
/// </summary>
public class BallAudioManager : MonoBehaviour
{

    [SerializeField, Header("音源")] AudioClip boundSound;
    [SerializeField] AudioClip breakBlockSound;
    [SerializeField] AudioClip missSound;

    [SerializeField, Header("音を流すオブジェクト")]
    private AudioSource audioSource;

    public void BoundSound()
    {
        audioSource.PlayOneShot(boundSound);
    }

    public void HitBlockSound()
    {
        audioSource.PlayOneShot(breakBlockSound);
    }

    public void MissSound()
    {
        audioSource.PlayOneShot(missSound);
    }
}
