using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̑��̃Q�[��SE�̍Đ��N���X
/// </summary>
public class OtherAudioManager : MonoBehaviour
{
    [SerializeField, Header("����")]
    private List<AudioClip> SoundEffects;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="SENumber">
    /// 0�F���[�v���ʉ�
    /// </param>
    internal void PlaySE(int SENumber)
    {
        audioSource.PlayOneShot(SoundEffects[SENumber]);
    }
}
