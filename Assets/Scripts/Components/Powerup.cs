using UnityEngine;
using System.Collections;
using JellyJam.Events;

[RequireComponent (typeof(AudioSource))]
public class Powerup : MonoBehaviour {

    public enum PowerupType { Gun }
    public PowerupType powerupType;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void InitPowerup(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnEnable()
    {
        JellyEventController.SubscribeEvent(JellyEventType.CollectGun, CollectPowerup);
    }

    private void OnDisable()
    {
        JellyEventController.UnsubscribeEvent(JellyEventType.CollectGun, CollectPowerup);
    }

    private void CollectPowerup()
    {
        AudioController.instance.PlayOneShot(AudioModel.instance.powerupSE);
        gameObject.SetActive(false);
    }

}
