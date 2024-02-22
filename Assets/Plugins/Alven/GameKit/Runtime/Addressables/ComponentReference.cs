#if GAMEKIT_ADDRESSABLES_INTEGRATION
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Alven.GameKit.Addressables
{
    /// <summary>
    /// Creates an AssetReference that is restricted to having a specific Component.
    /// * This is the class that inherits from AssetReference.  It is generic and does not specify which Components it might care about.  A concrete child of this class is required for serialization to work.
    /// * At edit-time it validates that the asset set on it is a GameObject with the required Component.
    /// * At runtime it can load/instantiate the GameObject, then return the desired component.  API matches base class (LoadAssetAsync &amp; InstantiateAsync).
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    [Serializable]
    public class ComponentReference<TComponent> : AssetReferenceGameObject
    {
        public ComponentReference(string guid) : base(guid)
        {
        }

        public new AsyncOperationHandle<TComponent> InstantiateAsync(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return UnityEngine.AddressableAssets.Addressables.ResourceManager.CreateChainOperation(base.InstantiateAsync(position, Quaternion.identity, parent), GameObjectReady);
        }

        public new AsyncOperationHandle<TComponent> InstantiateAsync(Transform parent = null, bool instantiateInWorldSpace = false)
        {
            return UnityEngine.AddressableAssets.Addressables.ResourceManager.CreateChainOperation(base.InstantiateAsync(parent, instantiateInWorldSpace), GameObjectReady);
        }

        public new AsyncOperationHandle<TComponent> LoadAssetAsync()
        {
            return UnityEngine.AddressableAssets.Addressables.ResourceManager.CreateChainOperation(base.LoadAssetAsync<GameObject>(), GameObjectReady);
        }

        private AsyncOperationHandle<TComponent> GameObjectReady(AsyncOperationHandle<GameObject> arg)
        {
            var comp = arg.Result.GetComponent<TComponent>();
            return UnityEngine.AddressableAssets.Addressables.ResourceManager.CreateCompletedOperation(comp, string.Empty);
        }

        public override bool ValidateAsset(Object obj)
        {
            return obj is GameObject go && go.GetComponent<TComponent>() != null;
        }

        public override bool ValidateAsset(string path)
        {
#if UNITY_EDITOR
            //this load can be expensive...
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return go != null && go.GetComponent<TComponent>() != null;
#else
        return false;
#endif
        }
    }
}
#endif