using UnityEngine;
using System.Collections;
using System;
using JellyJam.Events;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    public enum GameState { Game, Menu, End }

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
        JellyEventController.SubscribeEvent(JellyEventType.GameEnd, EndGame);
        JellyEventController.SubscribeEvent(JellyEventType.PlayerCollision, DoRestart);
        JellyEventController.SubscribeEvent(JellyEventType.EnemyDead, UpdateScore);
        JellyEventController.SubscribeEvent(JellyEventType.EnemyDead, UpdateNumOfEnemies);
    }

    private void UnsubscribeEvents()
    {
        JellyEventController.UnsubscribeEvent(JellyEventType.GameEnd, EndGame);
        JellyEventController.UnsubscribeEvent(JellyEventType.PlayerCollision, DoRestart);
        JellyEventController.UnsubscribeEvent(JellyEventType.EnemyDead, UpdateScore);
        JellyEventController.UnsubscribeEvent(JellyEventType.EnemyDead, UpdateNumOfEnemies);
    }

    private void Start()
    {
        DoMenu();
    }

    public GameState GetGameState()
    {
        return _currGameState;
    }

    private void EndGame()
    {
        _currGameState = GameState.End;
    }

    private void DoMenu()
    {
        _currGameState = GameState.Menu;

        UIController.instance.ResetUI();
        UIController.instance.ShowMenu();
    }

    private void DoStartGame()
    {
        _currGameState = GameState.Game;

        EnemiesController.SpawnNextRound();
        _currEnemies = EnemiesController.GetCurrentNumberOfEnemies();
        UpdateWaveNum();

        UIController.instance.UnshowMenu();
        UIController.instance.ShowGameUI();
        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    private void DoRestart()
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
                UIController.instance.UnshowGameUI();
                UIController.instance.ShowTheEnd();
            }
            else
            {
                UpdateWaveNum();
                UIController.instance.ShowNextWaveTransition(_currWaveNum, DoNextRound);
            }

        }

        UIController.instance.UpdateNumOfEnemies(_currEnemies);
    }

    private void DoNextRound()
    {
        EnemiesController.SpawnNextRound();
        _currEnemies = EnemiesController.GetCurrentNumberOfEnemies();
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

        if (_currGameState == GameState.End && Input.anyKeyDown)
        {
            UIController.instance.UnshowTheEnd();
            DoRestart();
        }
    }
}
