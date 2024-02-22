#if FISHNET
using System;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Alven.GameKit.FishNet
{
    public sealed class NetworkStateMachine : NetworkBehaviour
    {
        private readonly Dictionary<Type, NetworkStateBehaviour> _typeToState = new();

        private readonly SyncVar<NetworkStateBehaviour> _currentState = new();
        private NetworkStateBehaviour CurrentState
        {
            get => _currentState.Value;
            set => _currentState.Value = value;
        }

        private void Awake()
        {
            _currentState.OnChange += OnCurrentChanged;
        }

        private void OnDestroy()
        {
            _currentState.OnChange -= OnCurrentChanged;
        }

        [Server]
        public void Enter<TState>(Action<TState> configuration = null) where TState : NetworkStateBehaviour
        {
            var state = Get<TState>();
            configuration?.Invoke(state);
            Enter(state);
        }

        private void Enter(NetworkStateBehaviour state)
        {
            if (CurrentState)
            {
                CurrentState.Exit(true);
            }
            CurrentState = state;
            state.Enter(true);
        }

        private void OnCurrentChanged(NetworkStateBehaviour prev, NetworkStateBehaviour next, bool asServer)
        {
            if (asServer) return;
            
            if (prev)
            {
                prev.Exit(false);
            }
            next.Enter(false);
        }

        public override void OnStopClient()
        {
            if (CurrentState)
            {
                CurrentState.Exit(false);
            }
        }

        public override void OnStopServer()
        {
            if (CurrentState)
            {
                CurrentState.Exit(true);
            }
        }

        public override void OnStartServer()
        {
            CurrentState = null;
        }

        private TState Get<TState>() where TState : NetworkStateBehaviour
        {
            return _typeToState[typeof(TState)] as TState;
        }

        public void RegisterState(NetworkStateBehaviour state)
        {
            _typeToState[state.GetType()] = state;
            state.Initialize(this);
        }
    }
}
#endif