using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolTimer : MonoBehaviour
    {
        private float timeToReturn = 5f;

        public float TimeToReturn
        {
            get { return timeToReturn; }
            set { timeToReturn = Mathf.Max(0.1f, value); }
        }

        private void OnEnable()
        {
            Invoke("ReturnToPool", timeToReturn);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void ReturnToPool()
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }
}