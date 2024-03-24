using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolOnInvisible : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }
}