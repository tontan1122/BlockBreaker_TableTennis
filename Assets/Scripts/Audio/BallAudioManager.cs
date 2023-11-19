using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[���̉��Ǘ��N���X
/// </summary>
internal class BallAudioManager : MonoBehaviour
{
    [SerializeField,Header("����")]
    private List<AudioClip> ballSoundEffects;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="SENumber">
    /// 0�F�ǔ���SE
    /// 1�F�u���b�N�j��SE
    /// 2�F�~�X��SE
    /// </param>
    internal void PlayBallSE(int SENumber)
    {
        audioSource.PlayOneShot(ballSoundEffects[SENumber]);
    }
}
