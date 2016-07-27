using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PitchEffect : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
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
        return Mathf.Lerp(_minPitch, _maxPitch, TimeController.instance.TimeScale);
    }
}
