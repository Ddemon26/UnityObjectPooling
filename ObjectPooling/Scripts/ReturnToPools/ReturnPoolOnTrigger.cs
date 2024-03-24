using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolOnTrigger : MonoBehaviour
    {
        public string TriggerTag { get; private set; } = "";

        public void AssignDetails(string triggerTag)
        {
            TriggerTag = triggerTag;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrEmpty(TriggerTag) || other.CompareTag(TriggerTag))
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}