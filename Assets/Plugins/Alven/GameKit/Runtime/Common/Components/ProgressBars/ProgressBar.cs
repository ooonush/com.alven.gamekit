using System;
using UnityEngine;
using UnityEngine.UI;

namespace Alven.GameKit.Common.ProgressBars
{
    [AddComponentMenu("GameKit/ProgressBars/ProgressBar")]
    public class ProgressBar : MonoBehaviour
    {
        private enum FillModes { LocalScale, FillAmount, Width, Height, Anchor }

        [SerializeField] private FillModes _fillMode = FillModes.LocalScale;
        [Min(0)]
        [SerializeField] private float _increaseDelaySeconds;
        [Min(0)]
        [SerializeField] private float _decreaseDelaySeconds;
        [SerializeField] private ProgressBehaviour _progress;

        private float _currentProgress;
        private Vector2 _initialLocalScale;
        private Vector2 _initialSize;
        private Image _image;

        private void Awake()
        {
            _initialLocalScale = transform.localScale;
            if (transform is RectTransform rectTransform)
            {
                _initialSize = rectTransform.rect.size;
            }

            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _currentProgress = _progress.Value;
        }

        private void LateUpdate()
        {
            float progress = _progress.Value;
            float currentDelay = progress > _currentProgress ? _increaseDelaySeconds : _decreaseDelaySeconds;
            _currentProgress = currentDelay == 0 ? progress : Mathf.MoveTowards(_currentProgress, progress, Time.deltaTime / currentDelay);
            
            switch (_fillMode)
            {
                case FillModes.LocalScale:
                    Vector3 localScale = transform.localScale;
                    transform.localScale = new Vector3(_initialLocalScale.x * _currentProgress, localScale.y, localScale.z);
                    break;
                case FillModes.FillAmount:
                    if (_image)
                    {
                        _image.fillAmount = _currentProgress;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                case FillModes.Width:
                    (transform as RectTransform)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _initialSize.x * _currentProgress);
                    break;
                case FillModes.Height:
                    (transform as RectTransform)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _initialSize.y * _currentProgress);
                    break;
                case FillModes.Anchor:
                    throw new NotImplementedException();
            }
        }
    }
}