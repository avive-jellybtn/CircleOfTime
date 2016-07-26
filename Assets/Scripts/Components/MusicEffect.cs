using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicEffect : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _audioSource.pitch = GetAudioPitch();
    }

    private float GetAudioPitch()
    {
        return Mathf.Lerp(GameParameters.MIN_MUSIC_PITCH, GameParameters.MAX_MUSIC_PITCH, TimeController.instance.TimeScale);
    }
}
