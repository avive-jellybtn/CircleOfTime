using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    private Vector3 _scale;
    [SerializeField] private float _scalePercentage;

    private void Awake()
    {
        _scale = transform.localScale;
    }

    private void Start()
    {
        DoScaleAnimation();
    }

    private void DoScaleAnimation()
    {
        transform.DOScale(_scale.x * _scalePercentage, 0.37f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
    
}
