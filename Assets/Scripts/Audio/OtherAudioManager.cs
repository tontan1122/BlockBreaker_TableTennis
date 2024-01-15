/// <summary>
/// ���̑��̃Q�[��SE�̍Đ��N���X
/// </summary>
public class OtherAudioManager : AudioManager
{
    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="SENumber">
    /// 0�F���[�v����Ƃ�
    /// 1�F���[�v�o��Ƃ�
    /// </param>
    public void PlaySE(int SENumber)
    {
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
