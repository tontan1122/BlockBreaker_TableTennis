using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField, Header("イントロBGM")]
    private AudioSource introAudioSource;
    [SerializeField,Header("ループBGM")]
    private AudioSource loopAudioSource;

    private void Start()
    {
        //イントロ部分の再生開始
        introAudioSource.PlayScheduled(AudioSettings.dspTime);

        //イントロ終了後にループ部分の再生を開始
        loopAudioSource.PlayScheduled(AudioSettings.dspTime + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
    }
}
