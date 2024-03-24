using UnityEngine;

namespace Damon.ObjectRecycling
{
    public class ReturnPoolOnAnimationEnd : MonoBehaviour
    {
        public string animationName;

        private Animation _animation;

        private void Start()
        {
            _animation = GetComponent<Animation>();
        }

        private void Update()
        {
            if (!_animation.IsPlaying(animationName))
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}