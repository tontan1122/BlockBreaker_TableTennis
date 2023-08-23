using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField, Header("�C���g��BGM")]
    private AudioSource introAudioSource;
    [SerializeField,Header("���[�vBGM")]
    private AudioSource loopAudioSource;

    private void Start()
    {
        //�C���g�������̍Đ��J�n
        introAudioSource.PlayScheduled(AudioSettings.dspTime);

        //�C���g���I����Ƀ��[�v�����̍Đ����J�n
        loopAudioSource.PlayScheduled(AudioSettings.dspTime + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
    }
}
