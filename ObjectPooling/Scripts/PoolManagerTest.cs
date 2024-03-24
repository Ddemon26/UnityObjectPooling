using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class PoolManagerTest : MonoBehaviour
    {
        public PoolSettings settings;
        public GameObject prefab;
        public int poolSize = 10;
        public float spawnInterval = 1f;
        public bool becomeChildOnAwake = false;

        private float timeSinceLastSpawn;

        private void Start()
        {
            // Create a pool
            PoolManager.Instance.CreatePool(prefab, poolSize, settings);
        }

        private void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                // Get an object from the pool
                var obj = PoolManager.Instance.GetObject(prefab, settings, becomeChildOnAwake ? transform : null);

                // Set the position of the object to the position of this GameObject
                obj.transform.position = transform.position;

                // Reset the timer
                timeSinceLastSpawn = 0f;
            }
        }
    }
}