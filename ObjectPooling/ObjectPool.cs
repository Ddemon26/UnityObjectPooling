using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly Queue<GameObject> objectPool = new();
    private readonly GameObject prefab;
    public ObjectPool(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        for (int i = 0; i < poolSize; i++)
        {
            objectPool.Enqueue(CreatePooledObject());
        }
    }
    public GameObject GetAndActivateObject()
    {
        if (objectPool.Count == 1)
        {
            // Add a new object to the pool when only one object is left
            objectPool.Enqueue(CreatePooledObject());
        }
        return ActivateObject(objectPool.Dequeue());
    }
    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
    private GameObject CreatePooledObject()
    {
        var obj = GameObject.Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }
    private GameObject ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
        return obj;
    }
}
