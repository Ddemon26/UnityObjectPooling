using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolOnOutOfBounds : MonoBehaviour
    {
        public float boundary = 50f;

        private void Update()
        {
            if (Mathf.Abs(transform.position.x) > boundary || Mathf.Abs(transform.position.y) > boundary || Mathf.Abs(transform.position.z) > boundary)
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}