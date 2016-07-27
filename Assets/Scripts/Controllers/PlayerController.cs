using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxRotSpeed;
    [SerializeField] private Player _player;
    [SerializeField] private Gun _mainGun;

    private List<Gun> _gunsList = new List<Gun>();
    private bool _canShoot = true;
    private int _currGunAngle;
    private int _numOfGuns;

    public static event Action OnCollision;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _player.OnCollectPowerup += DoOnCollectPowerup;
        _player.OnCollision += DoOnCollision;
    }

    private void UnsubscribeEvents()
    {
        _player.OnCollectPowerup -= DoOnCollectPowerup;
        _player.OnCollision -= DoOnCollision;
    }

    public void ResetPlayer()
    {
        ResetGuns();
        _player.ResetPlayer();

        _canShoot = true;
    }

    public Vector3 GetPlayerPos()
    {
        return _player.GetPlayerPos();
    }

    private void Update()
    {
        if (GameController.instance.GetGameState() != GameController.GameState.Game)
            return;

        SetPlayerColor();
        MovePlayer();
        RoatatePlayer();
        ShootBullets();
        BoundPlayerToScreen();
    }


    private void MovePlayer()
    {
        if (Input.GetKey(GameParameters.UP_KEYCODE))
        {
            _player.UpdatePlayerMove(Player.PlayerDirection.Up, GetCurrentPlayerMovementSpeed());
        }
        else if (Input.GetKey(GameParameters.DOWN_KEYCODE))
        {
            _player.UpdatePlayerMove(Player.PlayerDirection.Down, GetCurrentPlayerMovementSpeed());
        }

    }
    private void RoatatePlayer()
    {
        if (Input.GetKey(GameParameters.LEFT_KEYCODE))
        {
            _player.UpdatePlayerRotation(KeyCode.LeftArrow, GetCurrentPlayerRotationSpeed());
        }
        else if (Input.GetKey(GameParameters.RIGHT_KEYCODE))
        {
            _player.UpdatePlayerRotation(KeyCode.RightArrow, GetCurrentPlayerRotationSpeed());
        }
    }

    private void SetPlayerColor()
    {
        _player.SetPlayerColor(GetCurrentPlayerColor());
    }

    private float GetCurrentPlayerMovementSpeed()
    {
        float currPlayerSpeed = Mathf.Lerp(0, _maxSpeed, TimeController.instance.TimeScale);
        return currPlayerSpeed;
    }

    private float GetCurrentPlayerRotationSpeed()
    {
        float currRotSpeed = Mathf.Lerp(0, _maxRotSpeed, TimeController.instance.TimeScale);
        return currRotSpeed;
    }

    public Color32 GetCurrentPlayerColor()
    {
        return Color32.Lerp(GameParameters.START_PLAYER_COLOR, GameParameters.END_PLAYER_COLOR, TimeController.instance.TimeScale);
    }

    private void ShootBullets()
    {
        if (!_canShoot)
            return;

        if (Input.GetKeyDown(GameParameters.SHOOT_KEYCODE))
        {
            _canShoot = false;

            _mainGun.ShootBullet();

            foreach (Gun gun in _gunsList)
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
        float clampX = Mathf.Clamp(_player.transform.position.x, -BoundariesController.ScreenWidth, BoundariesController.ScreenWidth);
        float clampY = Mathf.Clamp(_player.transform.position.y, -BoundariesController.ScreenHeight, BoundariesController.ScreenHeight);
        _player.SetPlayerPos(new Vector2(clampX, clampY));
    }

    public void DoOnCollision()
    {
        if (OnCollision != null)
        {
            OnCollision();
        }
    }

    public void DoOnCollectPowerup(Powerup.PowerupType pt)
    {
        switch (pt)
        {
            case Powerup.PowerupType.Gun:
                AddGun();
                break;
        }
    }

    private void AddGun()
    {
        if (_numOfGuns == 3)
            return;

        _numOfGuns++;
        _currGunAngle += 90;
        Gun gun = PoolsManager.Instance.GetPrefab(PoolEnums.GunComponent).GetComponent<Gun>();
        gun.SetGunPosition(_player.transform, _currGunAngle);
        _gunsList.Add(gun);
     }

    private void ResetGuns()
    {
        _numOfGuns = 0;
        _currGunAngle = 0;

        foreach (Gun gun in _gunsList)
        {
            gun.gameObject.SetActive(false);
        }

        _gunsList.Clear();
    }
}

