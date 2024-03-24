using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class PoolManagerTest : MonoBehaviour
    {
        [SerializeField] PoolSettings settings;
        [SerializeField] GameObject prefab;
        [SerializeField] float spawnInterval = 1f;

        private float timeSinceLastSpawn;

        private void Start()
        {
            // Create a pool
            PoolManager.Instance.CreatePool(prefab, settings.poolSize, settings);
        }

        private void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                // Get an object from the pool
                var obj = PoolManager.Instance.GetObject(prefab, settings, settings.becomeChildOnAwake ? transform : null);

                // Set the position of the object to the position of this GameObject
                obj.transform.position = transform.position;

                // Reset the timer
                timeSinceLastSpawn = 0f;
            }
        }
    }
}