using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform _gunTransform;

    public void SetGunPosition(float angle)
    {

    }

    public void ShootBullet()
    {
        Bullet newBullet = PoolsManager.Instance.GetPrefab(PoolEnums.Bullet).GetComponent<Bullet>();
        if (newBullet != null)
        {
            newBullet.ShootBullet(_gunTransform.position, _gunTransform.up);
        }
    }

    public void ShootEnemyBullet()
    {
        Bullet newBullet = PoolsManager.Instance.GetPrefab(PoolEnums.EnemyBullet).GetComponent<Bullet>();
        if (newBullet != null)
        {
            newBullet.ShootBullet(_gunTransform.position, _gunTransform.up);
        }
    }
}
