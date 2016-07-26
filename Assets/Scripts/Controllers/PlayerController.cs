using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Player _player;
    [SerializeField] private Gun[] _guns;
    private bool _canShoot = true;

    public static event Action OnCollision;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void ResetPlayer()
    {
        _player.ResetPlayer();
    }

    public Vector3 GetPlayerPos()
    {
        return _player.GetPlayerPos();
    }

    private void Update()
    {
        if (GameManager.instance.GetGameState() != GameManager.GameState.Game)
            return;

        SetPlayerColor();
        MovePlayer();
        RoatatePlayer();
        ShootBullets();
        BoundPlayerToScreen();
    }


    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _player.UpdatePlayerMove(Player.PlayerDirection.Up, GetCurrentPlayerSpeed());
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _player.UpdatePlayerMove(Player.PlayerDirection.Down, GetCurrentPlayerSpeed());
        }

    }
    private void RoatatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _player.UpdatePlayerRotation(KeyCode.LeftArrow);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _player.UpdatePlayerRotation(KeyCode.RightArrow);
        }
    }

    private void SetPlayerColor()
    {
        _player.SetPlayerColor(GetCurrentPlayerColor());
    }

    private float GetCurrentPlayerSpeed()
    {
        float currPlayerSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, TimeController.instance.TimeScale);
        return currPlayerSpeed;
    }

    public Color32 GetCurrentPlayerColor()
    {
        return Color32.Lerp(GameParameters.START_PLAYER_COLOR, GameParameters.END_PLAYER_COLOR, TimeController.instance.TimeScale);
    }

    private void ShootBullets()
    {
        if (!_canShoot)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _canShoot = false;

            foreach (Gun gun in _guns)
            {
                gun.ShootBullet();
            }

            StartCoroutine(EnableBullet(0.3f));
        }
    }

    private IEnumerator EnableBullet(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _canShoot = true;
    }

    private void BoundPlayerToScreen()
    {
        float clampX = Mathf.Clamp(_player.transform.position.x, -BoundariesManager.ScreenWidth, BoundariesManager.ScreenWidth);
        float clampY = Mathf.Clamp(_player.transform.position.y, -BoundariesManager.ScreenHeight, BoundariesManager.ScreenHeight);
        _player.SetPlayerPos(new Vector2(clampX, clampY));
    }

    public static void DoOnCollision()
    {
        if (OnCollision != null)
        {
            OnCollision();
        }
    }
}

