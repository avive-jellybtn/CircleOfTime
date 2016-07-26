using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Green,
        Yellow,
        Red,
        Boss
    }
    public EnemyType enemyType;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private SpriteRenderer _enemyOutlineRenderer;

    private float _minEnemySpeed;
    private float _enemySpeed;
    private float _numberOfHits;
    private int _currNumOfHits;
    private Vector3 _startSize;
    private bool _canColorOutline  = true;

    public static event Action OnEnemyDead;

    private void Awake()
    {
        _startSize = _enemy.transform.localScale;
    }

    private void OnEnable()
    {
        SetEnemy();
        ShowEnemy();
    }

    private void SetEnemy()
    {
        switch (enemyType)
        {
            case EnemyType.Green:
                _minEnemySpeed = -0.1f;
                _enemySpeed = -0.5f;
                _numberOfHits = 3;
                break;
            case EnemyType.Red:
                _minEnemySpeed = -0.2f;
                _enemySpeed = -1.5f;
                _numberOfHits = 1;
                break;
            case EnemyType.Yellow:
                _minEnemySpeed = -0.15f;
                _enemySpeed = -1f;
                _numberOfHits = 2;
                break;
            case EnemyType.Boss:
                _minEnemySpeed = 0.2f;
                _enemySpeed = 1.5f;
                _numberOfHits = 5;
                break;
        }
    }

    private void ShowEnemy()
    {
        _enemy.transform.localScale = Vector3.zero;
        _enemy.transform.DOScale(_startSize, 1f).SetEase(Ease.OutQuad);
    }

    private void UnshowEnemy()
    {
        _enemy.transform.DOScale(Vector3.zero, 0.37f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }
        );
    }

    private void Update()
    {
        _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, PlayerController.instance.GetPlayerPos(), GetEnemySpeed() * Time.deltaTime);
    }

    private float GetEnemySpeed()
    {
        return Mathf.Lerp(_minEnemySpeed, _enemySpeed, TimeController.instance.TimeScale);
    }

    public void HitEnemy()
    {
        _currNumOfHits++;

        StartCoroutine(UpdateOutlineColor());

        if (_currNumOfHits == _numberOfHits)
        {
            UnshowEnemy();

            if (OnEnemyDead != null)
            {
                OnEnemyDead();
            }
        }
    }

    private IEnumerator UpdateOutlineColor()
    {
        if (!_canColorOutline)
            yield return null;

        _canColorOutline = false;

        Color32 originalColor = _enemyOutlineRenderer.color;
        _enemyOutlineRenderer.color = new Color32(231, 76, 60,255);

        yield return new WaitForSeconds(0.05f);

        _enemyOutlineRenderer.color = originalColor;
        _canColorOutline = true;
    }


    public void SetEnemyDistance(float d)
    {
        _enemy.transform.localPosition = new Vector3(0, d, 0);
    }
}
