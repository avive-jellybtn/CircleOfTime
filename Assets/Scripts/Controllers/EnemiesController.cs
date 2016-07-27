using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EnemiesController
{
    private static int _currLevelIndex;
    private static int _currNumOfEnemies;

    public static void Reset()
    {
        _currLevelIndex = 0;
    }

    public static void SpawnNextRound()
    {
        _currLevelIndex++;
        _currNumOfEnemies = 0;

        LevelsModel.Level currLevel = LevelsModel.GetLevel(_currLevelIndex);

        Dictionary<PoolEnums, int> numOfEnemiesDic = new Dictionary<PoolEnums, int>();
        numOfEnemiesDic.Add(PoolEnums.RedEnemy, currLevel.numOfRedEnemies);
        numOfEnemiesDic.Add(PoolEnums.YellowEnemy, currLevel.numOfYellowEnemies);
        numOfEnemiesDic.Add(PoolEnums.GreenEnemy, currLevel.numOfGreenEnemies);
        numOfEnemiesDic.Add(PoolEnums.BossEnemy, currLevel.numOfBossEnemies);

        foreach (var numOfEnemies in numOfEnemiesDic)
        {
            for (int i = 0; i < numOfEnemies.Value; i++)
            {
                SpawnEnemy(numOfEnemies.Key);
                _currNumOfEnemies++;
            }
        }

        if (currLevel.spawnPowerup)
        {
            SpawnPowerup();
        }
    }

    public static int GetCurrentNumberOfEnemies()
    {
        return _currNumOfEnemies;
    }

    private static void SpawnEnemy(PoolEnums enemyEnum) 
    {
        Enemy tempEnemy = PoolsManager.Instance.GetPrefab(enemyEnum).GetComponent<Enemy>();
        if (tempEnemy != null)
        {
            tempEnemy.InitEnemy(PlayerController.instance.GetPlayerPos(), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))), Random.Range(3f, 7f));
        }
    }

    private static void SpawnPowerup()
    {
        GameObject tempPowerup = PoolsManager.Instance.GetPrefab(PoolEnums.GunPowerup);
        tempPowerup.transform.position = new Vector3(Random.Range(-BoundariesController.ScreenWidth, BoundariesController.ScreenWidth), Random.Range(-BoundariesController.ScreenHeight, BoundariesController.ScreenHeight), 0);
    }
}
