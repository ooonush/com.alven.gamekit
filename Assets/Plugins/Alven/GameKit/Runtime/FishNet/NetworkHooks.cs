#if FISHNET
using System;
using FishNet.Connection;
using FishNet.Object;

namespace Alven.GameKit.FishNet
{
    public class NetworkHooks : NetworkBehaviour
    {
        public event Action OnNetworkStarted;
        public event Action OnNetworkStopped;
        public event Action OnServerStarted;
        public event Action OnServerStopped;
        public event Action OnClientStarted;
        public event Action OnClientStopped;
        public event Action<NetworkConnection> OnClientOwnership;
        public event Action<NetworkConnection> OnServerOwnership;

        public override void OnStartNetwork() => OnNetworkStarted?.Invoke();
        public override void OnStopNetwork() => OnNetworkStopped?.Invoke();
        public override void OnStartServer() => OnServerStarted?.Invoke();
        public override void OnStopServer() => OnServerStopped?.Invoke();
        public override void OnStartClient() => OnClientStarted?.Invoke();
        public override void OnStopClient() => OnClientStopped?.Invoke();

        public override void OnOwnershipClient(NetworkConnection prevOwner) => OnClientOwnership?.Invoke(prevOwner);
        public override void OnOwnershipServer(NetworkConnection prevOwner) => OnServerOwnership?.Invoke(prevOwner);
    }
}
#endif