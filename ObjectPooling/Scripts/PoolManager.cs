using System;
using UnityEngine;

namespace Damon.ObjectRecycling
{
    public enum ReturnPoolType {Timer, Collision, Trigger, Input, None }

    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance;
        public static PoolManager Instance { get { return instance; } }

        private ObjectPoolCreator poolCreator = new ObjectPoolCreator();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public void CreatePool(GameObject prefab, int poolSize)
        {
            poolCreator.InitializePool(prefab, poolSize);
        }

        public GameObject GetObject(GameObject prefab, Transform newParent = null)
        {
            return poolCreator.GetPooledObject(prefab, newParent);
        }

        public void ReturnObject(GameObject obj)
        {
            poolCreator.ReturnObjectToPool(obj);
        }
    }
}