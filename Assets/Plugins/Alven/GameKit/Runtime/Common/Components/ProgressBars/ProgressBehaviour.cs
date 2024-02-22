using UnityEngine;
using UnityEngine.Events;

namespace Alven.GameKit.Common.ProgressBars
{
    [AddComponentMenu("GameKit/ProgressBars/ProgressBehaviour")]
    public class ProgressBehaviour : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _value = 1f;
        [SerializeField] private UnityEvent<float> _onIncreased;
        [SerializeField] private UnityEvent<float> _onDecreased;
        [SerializeField] private UnityEvent<float> _onChanged;

        public float Value
        {
            get => _value;
            set
            {
                switch (value)
                {
                    case < 0:
                        Debug.LogWarning("Value cannot be less than 0");
                        break;
                    case > 1:
                        Debug.LogWarning("Value cannot be greater than 1");
                        break;
                }
                
                float clampValue = Mathf.Clamp01(value);
                
                float change = clampValue - _value;
                if (Mathf.Abs(change) < float.Epsilon) return;
                _value = clampValue;
                
                if (change > 0)
                {
                    _onIncreased.Invoke(change);
                }
                else
                {
                    _onDecreased.Invoke(change);
                }
                _onChanged.Invoke(change);
            }
        }

        public UnityEvent<float> OnIncreased => _onIncreased;
        public UnityEvent<float> OnDecreased => _onDecreased;
        public UnityEvent<float> OnChanged => _onChanged;
    }
}