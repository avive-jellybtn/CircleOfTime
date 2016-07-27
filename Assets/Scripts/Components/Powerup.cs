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

    private void OnEnable()
    {
        JellyEventController.SubscribeEvent(JellyEventType.CollectPowerup, CollectPowerup);
    }

    private void OnDisable()
    {
        JellyEventController.UnsubscribeEvent(JellyEventType.CollectPowerup, CollectPowerup);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(GameParameters.PLAYER_LAYER))
        {
            JellyEventController.FireEvent(JellyEventType.CollectPowerup);
        }
    }

    private void CollectPowerup()
    {
        AudioController.instance.PlayOneShot(AudioModel.instance.powerupSE);
        gameObject.SetActive(false);
    }

}
