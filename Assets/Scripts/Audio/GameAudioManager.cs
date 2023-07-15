using UnityEngine;

/// <summary>
/// �Q�[���̃V�X�e���I�[�f�B�I
/// </summary>
public class GameAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField, Header("�{�^�������Ƃ���SE")]private AudioClip puchButtonSE;
    [SerializeField, Header("�X�e�[�W�ړ���SE")] private AudioClip moveStageSE;
    [SerializeField, Header("�X�e�[�W�N���A����SE")] private AudioClip StageClearSE;

    public void ClickButtonAudio()
    {
        audioSource.PlayOneShot(puchButtonSE);
    }

    public void MoveStageAudio()
    {
        audioSource.PlayOneShot(moveStageSE);
    }

    public void ClearStageAudio()
    {
        audioSource.PlayOneShot(StageClearSE);
    }
}
