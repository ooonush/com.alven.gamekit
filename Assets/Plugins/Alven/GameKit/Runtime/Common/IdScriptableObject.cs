using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Alven.GameKit.Common
{
    /// <summary>
    /// ScriptableObject that stores a GUID for unique identification. The population of this field is implemented
    /// inside an Editor script.
    /// </summary>
    [Serializable]
    public abstract class IdScriptableObject : ScriptableObject
    {
        // [HideInInspector]
        [SerializeField] private string _id;

        public string Id => _id;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(_id))
            {
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out _id, out long _);
            }
#endif
        }
    }
}
