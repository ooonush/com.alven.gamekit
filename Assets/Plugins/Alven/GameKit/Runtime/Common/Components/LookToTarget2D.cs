using UnityEngine;

namespace Alven.GameKit.Common
{
    public class LookToTarget2D : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private bool _initialLookRight;
        private bool _isInitialPositiveX;

        private void Awake()
        {
            _initialLookRight = _target.position.x >= transform.position.x;
            _isInitialPositiveX = _target.localScale.x >= 0;
        }

        private void LateUpdate()
        {
            bool isLookRight = _target.position.x >= transform.position.x;
            bool isPositiveX = transform.localScale.x >= 0;
            if (isLookRight == _initialLookRight)
            {
                if (isPositiveX != _isInitialPositiveX)
                {
                    FlipX();
                }
            }
            else
            {
                if (isPositiveX == _isInitialPositiveX)
                {
                    FlipX();
                }
            }
        }

        private void FlipX()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}