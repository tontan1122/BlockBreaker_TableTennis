/// <summary>
/// �{�[���̉��Ǘ��N���X
/// </summary>
public class BallAudioManager : AudioManager
{
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
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
