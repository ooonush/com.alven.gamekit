using UnityEngine;

namespace Alven.GameKit.Common
{
    [ExecuteAlways]
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] public Transform Target;
        [SerializeField] private float _speed;
        private Vector3 _position;

        private void OnEnable()
        {
            _position = transform.position;
        }

        private void LateUpdate()
        {
            if (!Target) return;
            _position = Vector3.MoveTowards(_position, Target.position, _speed * Time.deltaTime);
            transform.position = _position;
        }
    }
}