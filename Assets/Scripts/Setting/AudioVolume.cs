using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// �I�[�f�B�I�ݒ���s���N���X
/// </summary>
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField, Header("BGM��Slider")]
    private Slider bgmSlider;
    [SerializeField, Header("SE��Slider")]
    private Slider seSlider;
    [SerializeField, Header("Slider�̒l�̑傫��")]
    float maxValue = 80;

    private static readonly int BGM_VALUE = 65;  // �N����BGM�̉���
    private static readonly int SE_VALUE = 70;  // �N����SE�̉���

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
