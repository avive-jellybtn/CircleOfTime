using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController instance
    { get
        {
            return _instance;
        }
    }

    private AudioSource _soundEffectsAudioSource;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;

        _soundEffectsAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1.0f)
    {
        _soundEffectsAudioSource.PlayOneShot(clip, volume);
    }
}
