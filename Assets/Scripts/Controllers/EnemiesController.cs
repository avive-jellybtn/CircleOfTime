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
                GameObject tempEnemy = PoolsManager.Instance.GetPrefab(numOfEnemies.Key);
                tempEnemy.transform.position = Vector3.zero;
                tempEnemy.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
                tempEnemy.GetComponent<Enemy>().SetEnemyDistance(Random.Range(4f, 7f));

                _currNumOfEnemies++;
            }
        }
    }

    public static int GetCurrentNumberOfEnemies()
    {
        return _currNumOfEnemies;
    }
}
