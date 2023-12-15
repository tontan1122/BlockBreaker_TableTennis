using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Header("����")]
    protected List<AudioClip> soundEffects;

    protected AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
