#if GAMEKIT_INPUTSYSTEM_INTEGRATION
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Alven.GameKit.InputSystem
{
    /// <summary>
    /// A button that is visually represented on-screen and triggered by touch or other pointer
    /// input.
    /// </summary>
    [AddComponentMenu("GameKit/Input/On-Screen Button")]
    public class OnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEvent _onUp;
        [SerializeField] private UnityEvent _onDown;
        
        public UnityEvent OnUp => _onUp;
        public UnityEvent OnDown => _onDown;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
            _onUp?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1.0f);
            _onDown?.Invoke();
        }

        [InputControl(layout = "Button")]
        [SerializeField]
        private string _controlPath;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }
    }
}
#endif