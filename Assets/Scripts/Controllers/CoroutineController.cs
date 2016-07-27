using UnityEngine;
using System.Collections;
using System;

public class CoroutineController : MonoBehaviour {

    private static CoroutineController _instance;
    public static CoroutineController instance
    {
        get
        {
            return _instance;
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

    public IEnumerator ExecuteOnEndOfFrame(Action a)
    {
        yield return new WaitForEndOfFrame();
        a();
    }
}
