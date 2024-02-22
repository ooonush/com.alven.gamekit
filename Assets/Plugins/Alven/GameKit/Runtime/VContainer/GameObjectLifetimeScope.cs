using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alven.GameKit.VContainer.Unity
{
    public class GameObjectLifetimeScope : LifetimeScope
    {
        [SerializeField] private MonoInstaller[] _monoInstallers;

        protected override void Awake()
        {
            if (!IsRoot && parentReference.Type != null && parentReference.Object == null)
            {
                if (transform.parent)
                {
                    parentReference.Object = transform.parent.GetComponentInParent(parentReference.Type, true) as LifetimeScope;
                }
                else
                {
                    parentReference.Object = FindFirstObjectByType(parentReference.Type, FindObjectsInactive.Include) as LifetimeScope;
                }
            }
            
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            if (_monoInstallers == null) return;
            foreach (MonoInstaller installer in _monoInstallers)
            {
                installer.Install(builder);
            }
        }

        private void OnValidate()
        {
            if (autoInjectGameObjects != null && !autoInjectGameObjects.Contains(gameObject))
            {
                autoInjectGameObjects.Insert(0, gameObject);
            }
        }
    }
}