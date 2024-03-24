using System.Collections.Generic;
using UnityEngine;

namespace Damon.ObjectRecycling
{
    // Represents a single object pool.
    public class ObjectPool
    {
        private readonly Queue<GameObject> objectPool = new Queue<GameObject>();
        private readonly GameObject prefab;
        private readonly Transform parentContainer;

        public ObjectPool(GameObject prefab, int poolSize, Transform parentContainer)
        {
            this.prefab = prefab;
            this.parentContainer = parentContainer;

            for (int i = 0; i < poolSize; i++)
            {
                objectPool.Enqueue(CreatePooledObject());
            }
        }

        public GameObject GetAndActivateObject(Transform newParent = null)
        {
            if (objectPool.Count == 0)
            {
                objectPool.Enqueue(CreatePooledObject());
            }
            GameObject obj = ActivateObject(objectPool.Dequeue());
            obj.transform.SetParent(newParent); // Set the new parent
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.transform.SetParent(parentContainer);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        private GameObject CreatePooledObject()
        {
            GameObject obj = Object.Instantiate(prefab, parentContainer);
            obj.SetActive(false);
            // This is a key addition: attaching a PoolMember component that holds a reference to this pool.
            var poolMember = obj.AddComponent<PoolMember>();
            poolMember.SetPool(this);
            return obj;
        }

        private GameObject ActivateObject(GameObject obj)
        {
            obj.SetActive(true);
            return obj;
        }
    }
}