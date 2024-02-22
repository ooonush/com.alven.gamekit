#if FISHNET
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace Alven.GameKit.FishNet
{
    public class NetworkTimer : NetworkBehaviour
    {
        private readonly SyncTimer _syncTimer = new();

        public int RemainingSeconds => (int)_syncTimer.Remaining;
        public event NetworkAction OnStarted;
        public event NetworkAction OnFinished;

        [Server]
        public void StartTimer(int remainingSeconds)
        {
            _syncTimer.StartTimer(remainingSeconds);
        }

        private void Awake()
        {
            _syncTimer.OnChange += OnTimerChange;
        }

        private void Update()
        {
            _syncTimer.Update(Time.deltaTime);
        }

        private void OnTimerChange(SyncTimerOperation op, float prev, float next, bool asServer)
        {
            switch (op)
            {
                case SyncTimerOperation.Finished:
                    OnFinished?.Invoke(asServer);
                    break;
                case SyncTimerOperation.Start:
                    OnStarted?.Invoke(asServer);
                    break;
            }
        }
    }
}
#endif