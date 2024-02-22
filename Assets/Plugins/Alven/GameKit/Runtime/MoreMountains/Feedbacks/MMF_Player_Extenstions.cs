#if GAMEKIT_MM_FEEDBACKS_INTEGRATION
using MoreMountains.Feedbacks;

namespace Alven.GameKit.MoreMountains.Feedbacks
{
    public static class MMF_Player_Extenstions
    {
        public static bool StopAndResetFeedbacks(this MMF_Player feedbacks, bool stopAllFeedbacks = true)
        {
            if (!feedbacks.IsPlaying) return false;
            
            feedbacks.StopFeedbacks(stopAllFeedbacks);
            feedbacks.ResetFeedbacks();
            return true;
        }
    }
}
#endif