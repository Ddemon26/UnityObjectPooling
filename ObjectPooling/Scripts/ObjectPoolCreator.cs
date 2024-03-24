using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObjectPoolCreator
{
    protected readonly Dictionary<GameObject, ObjectPool> pools = new();
    public abstract void InitializePool(GameObject prefab, int poolSize);
    public abstract GameObject GetPooledObject(GameObject prefab, Transform newParent = null);
    public abstract void ReturnObjectToPool(GameObject prefab, GameObject obj);
    protected abstract void ValidatePrefab(GameObject prefab, bool checkPoolExists = false);
}

public class ObjectPoolCreator : AbstractObjectPoolCreator
{
    public override void InitializePool(GameObject prefab, int poolSize)
    {
        ValidatePrefab(prefab);
        if (pools.ContainsKey(prefab))
        {
            throw new System.ArgumentException("A pool for this prefab already exists.");
        }
        if (poolSize <= 0)
        {
            throw new System.ArgumentException("Pool size must be greater than 0.");
        }

        // Create a new GameObject to serve as the parent for the pooled objects
        GameObject poolContainer = new GameObject(prefab.name + " Pool");

        pools[prefab] = new ObjectPool(prefab, poolSize, poolContainer.transform);
    }
    public override GameObject GetPooledObject(GameObject prefab, Transform newParent = null)
    {
        ValidatePrefab(prefab, checkPoolExists: true);
        return pools[prefab].GetAndActivateObject(newParent);
    }

    public override void ReturnObjectToPool(GameObject prefab, GameObject obj)
    {
        ValidatePrefab(prefab, checkPoolExists: true);
        if (obj == null)
        {
            throw new System.ArgumentException("Object to return to pool cannot be null.");
        }
        pools[prefab].ReturnObject(obj);
    }
    protected override void ValidatePrefab(GameObject prefab, bool checkPoolExists = false)
    {
        if (prefab == null)
        {
            throw new System.ArgumentNullException(nameof(prefab), "Prefab cannot be null.");
        }
        if (checkPoolExists && !pools.ContainsKey(prefab))
        {
            throw new System.ArgumentException("Prefab not found in pool.");
        }
    }
}