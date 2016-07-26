using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour
{
    private Vector3 _dir;

    public static event Action OnCollision;

    public void ShootBullet(Vector3 initPos, Vector3 dir)
    {
        SetInitPos(initPos);
        SetDirection(dir);
    }

    private void SetInitPos(Vector3 pos)
    {
        transform.position = pos;
    }

    private void SetDirection(Vector3 tempDir)
    {
        _dir = tempDir;
    }

    private void Update()
    {
        transform.position += _dir * Time.deltaTime * GetBulletSpeed();
    }

    private float GetBulletSpeed()
    {
        return Mathf.Lerp(GameParameters.MIN_BULLET_SPEED, GameParameters.MAX_BULLET_SPEED, TimeController.instance.TimeScale);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.SetActive(false);
    }
}
