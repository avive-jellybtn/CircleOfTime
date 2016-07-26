using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public enum PlayerDirection { Up, Down }
    public event Action OnCollision;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        OnCollision += PlayerController.DoOnCollision;
    }

    private void OnDisable()
    {
        OnCollision -= PlayerController.DoOnCollision;
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void SetPlayerPos(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void UpdatePlayerMove(PlayerDirection pd, float speed)
    {
        if (pd == PlayerDirection.Up)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (pd == PlayerDirection.Down)
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }
    }

    public void UpdatePlayerRotation(KeyCode kc)
    {
        int sign = 0;

        if (kc == KeyCode.LeftArrow)
            sign = 1;

        if (kc == KeyCode.RightArrow)
            sign = -1;

        transform.Rotate(new Vector3(0, 0, sign * Time.deltaTime * 150));
    }

    public void SetPlayerColor(Color32 color)
    {
        _spriteRenderer.color = color;
    }

    public Vector3 GetPlayerPos()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (OnCollision != null)
        {
            OnCollision();
        }
    }
}
