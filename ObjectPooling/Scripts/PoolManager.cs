using System;
using UnityEngine;

namespace Damon.ObjectRecycling
{
    public enum ReturnPoolType { Timer, Collision, None }

    [Serializable]
    public class PoolSettings
    {
        public ReturnPoolType ReturnPoolType = ReturnPoolType.None;
        public float TimeToReturn = 5f;
        public string CollideTag = null;
    }

    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance;
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<PoolManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(PoolManager).Name);
                        instance = obj.AddComponent<PoolManager>();
                    }
                }
                return instance;
            }
        }

        private ObjectPoolCreator poolCreator = new ObjectPoolCreator();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes.
        }

        public void CreatePool(GameObject prefab, int poolSize, PoolSettings settings)
        {
            if (prefab == null || poolSize <= 0)
            {
                Debug.LogWarning("CreatePool: Invalid parameters.");
                return;
            }
            poolCreator.InitializePool(prefab, poolSize);

            // Assign the script to all objects in the pool
            foreach (var obj in poolCreator.GetAllPooledObjects(prefab))
            {
                AssignReturnPoolScript(obj, settings);
            }
        }


        public GameObject GetObject(GameObject prefab, PoolSettings settings, Transform newParent = null)
        {
            if (prefab == null || settings == null)
            {
                Debug.LogWarning("GetObject: Invalid parameters.");
                return null;
            }

            GameObject obj = poolCreator.GetPooledObject(prefab, newParent);
            AssignReturnPoolScript(obj, settings);
            return obj;
        }

        private void AssignReturnPoolScript(GameObject obj, PoolSettings settings)
        {
            if (obj == null || settings == null) return;

            switch (settings.ReturnPoolType)
            {
                case ReturnPoolType.Timer:
                    var timerScript = obj.GetComponent<ReturnPoolTimer>() ?? obj.AddComponent<ReturnPoolTimer>();
                    timerScript.TimeToReturn = settings.TimeToReturn;
                    break;
                case ReturnPoolType.Collision:
                    var collideScript = obj.GetComponent<ReturnPoolCollide>() ?? obj.AddComponent<ReturnPoolCollide>();
                    collideScript.AssignDetails(settings.CollideTag);
                    break;
                case ReturnPoolType.None:
                default:
                    break;
            }
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("ReturnObject: obj is null.");
                return;
            }
            poolCreator.ReturnObjectToPool(obj);
        }
    }
}