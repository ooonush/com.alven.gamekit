using System;

namespace Alven.GameKit.Common
{
    public interface ICallbackValue<T>
    {
        event Action OnValueChanged;
        T Value { get; set; }
    }
}