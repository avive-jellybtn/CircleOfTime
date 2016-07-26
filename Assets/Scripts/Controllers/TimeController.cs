using UnityEngine;
using System.Collections;
using System;

public class TimeController : MonoBehaviour
{
    [HideInInspector] public static TimeController instance;

    [SerializeField] private float _lerpTime;

    private float _timeScale;

    public float TimeScale
    {
        get
        {
            return _timeScale;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (_timeScale < 1)
            {
                _timeScale += UnityEngine.Time.deltaTime / _lerpTime;
            }
        }
        else
        {
            if (_timeScale > 0)
            {
                _timeScale -= UnityEngine.Time.deltaTime / _lerpTime;
            }
        }
    }
}
