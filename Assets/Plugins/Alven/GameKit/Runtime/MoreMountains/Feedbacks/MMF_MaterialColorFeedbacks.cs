#if GAMEKIT_MM_FEEDBACKS_INTEGRATION
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Alven.GameKit.MoreMountains.Feedbacks
{
    [FeedbackHelp("This feedback lets you control the color of a target Renderer Material over time.")]
    [FeedbackPath("Renderer/Material Color")]
    public class MMF_RendererMaterialColor : MMF_ColorFeedback
    {
        [MMFInspectorGroup("Target", true, 12, true)]
        [Tooltip("Renderer component to control")]
        public Renderer Renderer;

        public override bool EvaluateRequiresSetup() => Renderer == null;
        public override string RequiredTargetText => Renderer != null ? Renderer.name : "";
        protected override void AutomateTargetAcquisition() => Renderer = FindAutomatedTarget<Renderer>();

        protected override void SetColor(Color color) => Renderer.material.color = color;

        protected override Color GetColor() => Renderer.material.color;
    }
}
#endif