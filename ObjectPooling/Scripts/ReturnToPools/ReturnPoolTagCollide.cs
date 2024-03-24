using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolTagCollide : MonoBehaviour
    {
        public string TagToCollideWith { get; private set; } = "";

        public void AssignDetails(string tagToCollideWith)
        {
            TagToCollideWith = tagToCollideWith;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (string.IsNullOrEmpty(TagToCollideWith) || other.gameObject.tag == TagToCollideWith)
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}