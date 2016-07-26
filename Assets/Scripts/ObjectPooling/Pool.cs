using UnityEngine;
using System.Collections.Generic;
using System;

public class Pool
{

    private GameObject _prefab;
    private int _amount;
    private Transform _parent;
    private List<GameObject> _poolObjects;

    public Pool(GameObject prefab, int amount, Transform parent)
    {
        _prefab = prefab;
        _amount = amount;
        _parent = parent;
        _poolObjects = new List<GameObject>();

        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < _amount; i++)
        {
            InstantiatePrefab();
        }
    }

    private void InstantiatePrefab()
    {
        GameObject newGameObject = (GameObject)UnityEngine.Object.Instantiate(_prefab);
        newGameObject.transform.SetParent(_parent);
        newGameObject.SetActive(false);
        _poolObjects.Add(newGameObject);
        PoolMember poolMember = newGameObject.AddComponent<PoolMember>();
        poolMember.pool = this;
    }

    public void ReleaseObject(GameObject objectToRelease)
    {
        if (!_poolObjects.Contains(objectToRelease))
        {
            _poolObjects.Remove(objectToRelease);
        }
    }

    public GameObject Object
    {
        get
        {
            if (_poolObjects.Count < 1)
            {
                InstantiatePrefab();
            }
            GameObject clone = _poolObjects[0];
            _poolObjects.RemoveAt(0);
            clone.SetActive(true);
            return clone;
        }
    }
}

public class PoolMember : MonoBehaviour
{
    public Pool pool;

    void OnDisable()
    {
        pool.ReleaseObject(gameObject);
    }
}

