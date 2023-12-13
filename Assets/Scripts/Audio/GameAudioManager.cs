using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���̃V�X�e���I�[�f�B�I
/// </summary>
internal class GameAudioManager : AudioManager
{
    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="SENumber">
    /// 0�F�{�^���N���b�NSE
    /// 1�F�X�e�[�W�ړ�SE
    /// 2�F�X�e�[�W�N���ASE
    /// </param>
    public void PlayGameSE(int SENumber)
    {
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
