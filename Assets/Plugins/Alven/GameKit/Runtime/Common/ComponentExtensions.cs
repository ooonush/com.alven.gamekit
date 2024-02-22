using UnityEngine;

namespace Alven.GameKit.Common
{
    public static class ComponentExtensions
    {
        public static void Validate<T>(this Component component, ref T target) where T : Component
        {
            if (component == null) return;
            
            if (!target) target = component.GetComponent<T>();
        }

        public static void ValidateFromParent<T>(this Component component, ref T target) where T : Component
        {
            if (component == null) return;
            
            if (!target) target = component.GetComponentInParent<T>();
        }

        public static void ValidateFromChildren<T>(this Component component, ref T target) where T : Component
        {
            if (component == null) return;
            
            if (!target) target = component.GetComponentInChildren<T>();
        }
    }
}