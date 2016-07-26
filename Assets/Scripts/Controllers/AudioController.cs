using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [HideInInspector] public static AudioController instance;

    public AudioClip powerupSE;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
