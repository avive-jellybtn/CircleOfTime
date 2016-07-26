using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent (typeof(SpriteRenderer))]
public class BossColorLerp : MonoBehaviour {

    private List<Color32> _colorList;
    private int _currColorIndex;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _colorList = new List<Color32>();
        _colorList.Add(new Color32(26, 188, 156, 255));
        _colorList.Add(new Color32(52, 152, 219, 255));
        _colorList.Add(new Color32(231, 76, 60, 255));
        _colorList.Add(new Color32(46, 204, 113, 255));
        _colorList.Add(new Color32(241, 196, 15, 255));

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

	private void Start ()
    {
        DoColorLerp();   
	}

    private void DoColorLerp()
    {
        _spriteRenderer.DOColor(_colorList[_currColorIndex], 0.5f).OnComplete(() =>
        {
            _currColorIndex++;
            if (_currColorIndex > _colorList.Count-1)
            {
                _currColorIndex = 0;
            }

            DoColorLerp();
        }
   );
    }
	
	
}
