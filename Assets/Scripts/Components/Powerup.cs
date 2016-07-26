using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Powerup : MonoBehaviour {

    public enum PowerupType { Gun }
    public PowerupType powerupType;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(GameParameters.PLAYER_LAYER))
        {
            AudioController.instance.PlayOneShot(AudioController.instance.powerupSE);
            gameObject.SetActive(false);
        }
    }

}
