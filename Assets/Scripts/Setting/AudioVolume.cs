using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// オーディオ設定を行うクラス
/// </summary>
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField, Header("BGMのSlider")]
    Slider bgmSlider;
    [SerializeField, Header("SEのSlider")]
    Slider seSlider;
    [SerializeField, Header("Sliderの値の大きさ")]
    float maxValue = 80;

    public void AudioInit(float BGMValue, float SEValue)
    {
        bgmSlider.maxValue = maxValue;
        //seSlider.maxValue = maxValue;

        bgmSlider.value = BGMValue;
        seSlider.value = SEValue;

        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    private void Start()
    {
        //Init関数の中ではaudioMixerの設定がうまく行われなかったためStartに記述
        SetAudioMixerBGM(bgmSlider.value);
        SetAudioMixerSE(seSlider.value);
    }

    private void SetAudioMixerBGM(float value)
    {
        value -= maxValue;
        audioMixer.SetFloat("BGM", value);
    }

    private void SetAudioMixerSE(float value)
    {
        value -= maxValue;
        audioMixer.SetFloat("SE", value);
    }

    public float GetBGMValue
    {
        get { return bgmSlider.value; }
    }

    public float GetSEValue
    {
        get { return seSlider.value; }
    }
}
