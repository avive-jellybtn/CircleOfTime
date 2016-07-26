using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    public enum GameState { Gameover, Game, Menu }

    private GameState _currGameState;
    private int _currScore;
    private int _currEnemies;
    private int _currWaveNum;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;

        Application.targetFrameRate = 60;
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
        PlayerController.OnCollision += DoRestart;
        Enemy.OnEnemyDead += AddScore;
        Enemy.OnEnemyDead += UpdateNumOfEnemies;
    }

    private void UnsubscribeEvents()
    {
        PlayerController.OnCollision -= DoRestart;
        Enemy.OnEnemyDead -= AddScore;
        Enemy.OnEnemyDead -= UpdateNumOfEnemies;
    }

    private void Start()
    {
        DoMenu();
    }

    public GameState GetGameState()
    {
        return _currGameState;
    }

    public void DoMenu()
    {
        _currGameState = GameState.Menu;

        UIController.instance.ResetUI();
        UIController.instance.ShowMenu();
    }

    public void DoStartGame()
    {
        _currGameState = GameState.Game;

        EnemiesController.SpawnNextRound();
        _currEnemies = EnemiesController.GetCurrentNumberOfEnemies();

        UIController.instance.UnshowMenu();
        UIController.instance.ShowGameUI();
        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    public void DoGameOver()
    {
        _currGameState = GameState.Gameover;
    }

    public void DoRestart()
    {
        _currScore = 0;
        _currEnemies = 0;

        EnemiesController.Reset();
        PlayerController.instance.ResetPlayer();
        PoolsManager.Instance.ResetPools();

        DoMenu();
    }

    private void AddScore()
    {
        _currScore++;
        UIController.instance.UpdateScore(_currScore);
    }

    private void UpdateNumOfEnemies()
    {
        _currEnemies--;

        if (_currEnemies == 0)
        {
            EnemiesController.SpawnNextRound();
            _currEnemies = EnemiesController.GetCurrentNumberOfEnemies();
            _currWaveNum++;
        }

        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    private void Update()
    {
        if (_currGameState == GameState.Menu && Input.anyKeyDown)
        {
            DoStartGame();
        }

        if (_currGameState == GameState.Gameover && Input.anyKeyDown)
        {
            DoRestart();
        }
    }
}
