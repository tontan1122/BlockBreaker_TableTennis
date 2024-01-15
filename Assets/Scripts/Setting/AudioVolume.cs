using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// オーディオ設定を行うクラス
/// </summary>
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField, Header("BGMのSlider")]
    private Slider bgmSlider;
    [SerializeField, Header("SEのSlider")]
    private Slider seSlider;
    [SerializeField, Header("Sliderの値の大きさ")]
    float maxValue = 80;

    private static readonly int BGM_VALUE = 65;  // 起動時BGMの音量
    private static readonly int SE_VALUE = 70;  // 起動時SEの音量

    public void InitialAudioSettings(float BGMValue, float SEValue)
    {
        bgmSlider.maxValue = maxValue;

        if (BGMValue == 0 && SEValue == 0)
        {
            bgmSlider.value = BGM_VALUE;
            seSlider.value = SE_VALUE;
        }
        else
        {
            bgmSlider.value = BGMValue;
            seSlider.value = SEValue;
        }

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
