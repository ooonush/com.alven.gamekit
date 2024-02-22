#if GAMEKIT_MM_FEEDBACKS_INTEGRATION && GAMEKIT_MM_FEEDBACKS_SPINE_INTEGRATION
using MoreMountains.Feedbacks;
using Spine.Unity;
using UnityEngine;

namespace Alven.GameKit.MoreMountains.Feedbacks
{
    [FeedbackHelp("This feedback lets you control the color of a target SkeletonRenderer over time.")]
    [FeedbackPath("Spine/SkeletonRenderer/Skeleton Color")]
    public class MMF_SkeletonRendererColor : MMF_ColorFeedback
    {
        [MMFInspectorGroup("Target", true, 12, true)]
        [Tooltip("SkeletonRenderer component to control")]
        public SkeletonRenderer SkeletonRenderer;

        public override bool EvaluateRequiresSetup() => SkeletonRenderer == null;
        public override string RequiredTargetText => SkeletonRenderer != null ? SkeletonRenderer.name : "";
        protected override void AutomateTargetAcquisition() => SkeletonRenderer = FindAutomatedTarget<SkeletonRenderer>();

        protected override void SetColor(Color color)
        {
            SkeletonRenderer.skeleton.SetColor(color);
        }

        protected override Color GetColor()
        {
            return SkeletonRenderer.skeleton.GetColor();
        }
    }
}
#endif