#if GAMEKIT_MM_FEEDBACKS_INTEGRATION
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Alven.GameKit.MoreMountains.Feedbacks
{
    [FeedbackHelp("This feedback lets you control the color of a target Image over time.")]
    [FeedbackPath("UI/Image Color")]
    public class MMF_ImageColor : MMF_ColorFeedback
    {
        [MMFInspectorGroup("Target", true, 12, true)]
        [Tooltip("Image component to control")]
        public Image Image;

        public override bool EvaluateRequiresSetup() => Image == null;
        public override string RequiredTargetText => Image != null ? Image.name : "";
        protected override void AutomateTargetAcquisition() => Image = FindAutomatedTarget<Image>();

        protected override void SetColor(Color color) => Image.color = color;

        protected override Color GetColor() => Image.color;
    }
}
#endif