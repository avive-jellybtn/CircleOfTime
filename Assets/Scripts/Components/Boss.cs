using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    [SerializeField] private Gun[] _guns;

    private void Start()
    {
        StartCoroutine(ShootBullets(2.5f));
    }

    private IEnumerator ShootBullets(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        foreach (Gun gun in _guns)
        {
            gun.ShootEnemyBullet();
        }

        StartCoroutine(ShootBullets(2.5f));
    }

}
