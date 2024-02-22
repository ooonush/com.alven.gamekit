#if FISHNET
using System;
using FishNet.Object;
using FishNet.Utility.Extension;

namespace Alven.GameKit.FishNet
{
    public class NetworkStateBehaviour : NetworkBehaviour
    {
        protected NetworkStateMachine StateMachine;

        internal void Initialize(NetworkStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        private bool IsServerEntered { get; set; }
        private bool IsClientEntered { get; set; }
        private bool IsEntered => IsServerEntered || IsClientEntered;

        internal void Enter(bool asServer)
        {
            if (!NetworkManager.DoubleLogic(asServer))
            {
                EnterNetwork();
            }

            if (asServer)
            {
                EnterServerInternal();
            }
            else
            {
                EnterClientInternal();
            }
        }

        internal void Exit(bool asServer)
        {
            if (asServer)
            {
                ExitServerInternal();
            }
            else
            {
                ExitClientInternal();
            }

            if (!IsEntered)
            {
                ExitNetwork();
            }
        }

        private void EnterServerInternal()
        {
            if (IsServerEntered) throw new InvalidOperationException("State is already entered for server.");
            IsServerEntered = true;
            EnterServer();
        }

        private void EnterClientInternal()
        {
            if (IsClientEntered) throw new InvalidOperationException("State is already entered for client.");
            IsClientEntered = true;
            EnterClient();
        }

        private void ExitServerInternal()
        {
            ExitServer();
            IsServerEntered = false;
        }

        private void ExitClientInternal()
        {
            ExitClient();
            IsClientEntered = false;
        }

        protected virtual void EnterNetwork()
        {
        }

        protected virtual void ExitNetwork()
        {
        }

        protected virtual void EnterServer()
        {
        }

        protected virtual void EnterClient()
        {
        }

        protected virtual void ExitClient()
        {
        }

        protected virtual void ExitServer()
        {
        }
    }
}
#endif