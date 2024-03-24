using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolOnInput : MonoBehaviour
    {
        public KeyCode ReturnKey { get; private set; } = KeyCode.None;

        public void AssignDetails(KeyCode returnKey)
        {
            ReturnKey = returnKey;
        }

        private void Update()
        {
            if (ReturnKey == KeyCode.None || Input.GetKeyDown(ReturnKey))
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}