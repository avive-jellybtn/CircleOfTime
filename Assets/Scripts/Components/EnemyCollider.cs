using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyCollider : MonoBehaviour {

    private Enemy m_enemy;

    public void Init(Enemy enemy)
    {
        m_enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(GameParameters.BULLET_LAYER))
        {
            transform.DOShakePosition(0.15f, 0.05f, 30, 0);

            if (m_enemy != null)
            {
                m_enemy.HitEnemy();
            }
        }
    }
}
