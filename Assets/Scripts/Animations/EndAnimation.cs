using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;
using JellyJam.Events;

[RequireComponent (typeof(Animator))]
public class EndAnimation : MonoBehaviour {

    private Animator _animator;
    [SerializeField] private Text _anyKeyText;

	private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.speed = Mathf.Lerp(0.05f, 0.3f, TimeController.instance.TimeScale);
    }

    public void ShowAnyKeyToRestart()
    {
        _anyKeyText.DOFade(1, 1f).OnComplete(() =>
        {
            JellyEventController.FireEvent(JellyEventType.GameEnd);
        }
        );
    }

}
