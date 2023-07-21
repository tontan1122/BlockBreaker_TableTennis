using UnityEngine;

/// <summary>
/// �{�[���̉��Ǘ��N���X
/// </summary>
public class BallAudioManager : MonoBehaviour
{

    [SerializeField, Header("����")] AudioClip boundSound;
    [SerializeField] AudioClip breakBlockSound;
    [SerializeField] AudioClip missSound;

    [SerializeField, Header("���𗬂��I�u�W�F�N�g")]
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
