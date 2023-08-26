using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// �I�[�f�B�I�ݒ���s���N���X
/// </summary>
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField, Header("BGM��Slider")]
    Slider bgmSlider;
    [SerializeField, Header("SE��Slider")]
    Slider seSlider;
    [SerializeField, Header("Slider�̒l�̑傫��")]
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
        //Init�֐��̒��ł�audioMixer�̐ݒ肪���܂��s���Ȃ���������Start�ɋL�q
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
