using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PoolsManager : MonoBehaviour
{

    [HideInInspector]
    public static PoolsManager Instance;

    [Serializable]
    public struct PoolData
    {
        public GameObject prefab;
        public PoolEnums identifier;
        public int amount;
    }

    public List<PoolData> poolInfoList;

    private static Dictionary<PoolEnums, Pool> _poolsDict;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        InitPools();
    }

    private void InitPools()
    {
        _poolsDict = new Dictionary<PoolEnums, Pool>();

        foreach (PoolData pl in poolInfoList)
        {
            if (_poolsDict.ContainsKey(pl.identifier))
            {
                return;
            }

            Pool tempPool = new Pool(pl.prefab, pl.amount, transform);
            _poolsDict.Add(pl.identifier, tempPool);
        }
    }

    public GameObject GetPrefab(PoolEnums identifier)
    {
        if (!_poolsDict.ContainsKey(identifier))
        {
            return null;
        }

        return _poolsDict[identifier].Object;
    }

    public void ResetPools()
    {
        PoolMember[] poolObjects = GameObject.FindObjectsOfType<PoolMember>();
        foreach (PoolMember po in poolObjects)
        {
            po.gameObject.SetActive(false);
        }
    }

}
