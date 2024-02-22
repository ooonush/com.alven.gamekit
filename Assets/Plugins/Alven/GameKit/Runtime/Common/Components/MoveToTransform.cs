using UnityEngine;

namespace Alven.GameKit.Common
{
    public class MoveToTransform : MonoBehaviour
    {
        public Transform Target;

        private void LateUpdate()
        {
            if (Target)
            {
                transform.position = Target.position;
            }
        }
    }
}