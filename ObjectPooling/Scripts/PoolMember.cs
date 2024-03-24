using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class PoolMember : MonoBehaviour
    {
        private ObjectPool myPool;

        public void SetPool(ObjectPool pool)
        {
            myPool = pool;
        }

        public ObjectPool GetPool()
        {
            return myPool;
        }
    }
}