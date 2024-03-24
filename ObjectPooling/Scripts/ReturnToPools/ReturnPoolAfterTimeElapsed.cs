using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolAfterTimeElapsed : MonoBehaviour
    {
        public float timeToReturn = 10f;

        private void Start()
        {
            Invoke("ReturnToPool", timeToReturn);
        }

        private void ReturnToPool()
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }
}