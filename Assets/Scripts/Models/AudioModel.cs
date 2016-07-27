using UnityEngine;
using System.Collections;

public class AudioModel : MonoBehaviour
{
    private static AudioModel _instance;
    public static AudioModel instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private AudioClip _powerupSE;
    public AudioClip powerupSE
    {
        get
        {
            return _powerupSE;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }
}
