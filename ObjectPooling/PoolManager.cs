using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance { get { return instance; } }

    private Dictionary<GameObject, ObjectPool> pools = new Dictionary<GameObject, ObjectPool>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        if (prefab == null || poolSize <= 0)
        {
            throw new ArgumentException("Invalid arguments for CreatePool.");
        }
        if (pools.ContainsKey(prefab))
        {
            throw new ArgumentException("A pool for this prefab already exists.");
        }

        GameObject poolContainer = new GameObject(prefab.name + " Pool");
        poolContainer.transform.parent = this.transform;

        ObjectPool newPool = new ObjectPool(prefab, poolSize, poolContainer.transform);
        pools[prefab] = newPool;
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            throw new ArgumentException("Pool for this prefab does not exist.");
        }
        return pools[prefab].GetAndActivateObject();
    }

    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        if (!pools.ContainsKey(prefab) || obj == null)
        {
            throw new ArgumentException("Invalid arguments for ReturnObject.");
        }
        pools[prefab].ReturnObject(obj);
    }
}
