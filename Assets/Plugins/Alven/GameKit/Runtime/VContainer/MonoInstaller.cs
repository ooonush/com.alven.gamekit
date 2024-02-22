using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alven.GameKit.VContainer.Unity
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}