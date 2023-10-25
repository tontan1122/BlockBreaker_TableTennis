using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[���̉��Ǘ��N���X
/// </summary>
public class BallAudioManager : MonoBehaviour
{
    [SerializeField,Header("����")]
    private List<AudioClip> ballSoundEffects;

    [SerializeField, Header("���𗬂��I�u�W�F�N�g")]
    private AudioSource audioSource;

    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="SENumber">
    /// 0�F�ǔ���SE
    /// 1�F�u���b�N�j��SE
    /// 2�F�~�X��SE
    /// </param>
    public void PlayBallSE(int SENumber)
    {
        audioSource.PlayOneShot(ballSoundEffects[SENumber]);
    }
}
