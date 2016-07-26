using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    public enum GameState { Game, Menu }

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
        Enemy.OnEnemyDead += UpdateScore;
        Enemy.OnEnemyDead += UpdateNumOfEnemies;
    }

    private void UnsubscribeEvents()
    {
        PlayerController.OnCollision -= DoRestart;
        Enemy.OnEnemyDead -= UpdateScore;
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
        UpdateWaveNum();

        UIController.instance.UnshowMenu();
        UIController.instance.ShowGameUI();
        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    public void DoRestart()
    {
        _currScore = 0;
        _currEnemies = 0;
        _currWaveNum = 0;

        EnemiesController.Reset();
        PlayerController.instance.ResetPlayer();
        PoolsManager.Instance.ResetPools();
        UIController.instance.UnshowGameUI();

        DoMenu();
    }

    private void UpdateScore()
    {
        _currScore++;
        UIController.instance.UpdateScore(_currScore);
    }

    private void UpdateNumOfEnemies()
    {
        _currEnemies--;

        if (_currEnemies == 0)
        {
            if (_currWaveNum == LevelsModel.HighestLevelIndex())
            {
                DoRestart();
            }
            else
            {
                EnemiesController.SpawnNextRound();
                _currEnemies = EnemiesController.GetCurrentNumberOfEnemies();
                UpdateWaveNum();
            }
      
        }

        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    private void UpdateWaveNum()
    {
        _currWaveNum++;
        UIController.instance.UpdateWaveNum(_currWaveNum);
    }

    private void Update()
    {
        if (_currGameState == GameState.Menu && Input.anyKeyDown)
        {
            DoStartGame();
        }
    }
}
