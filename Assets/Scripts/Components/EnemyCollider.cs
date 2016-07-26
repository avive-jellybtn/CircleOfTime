using UnityEngine;
using System.Collections;

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
            if (m_enemy != null)
            {
                m_enemy.HitEnemy();
            }
        }
    }
}
