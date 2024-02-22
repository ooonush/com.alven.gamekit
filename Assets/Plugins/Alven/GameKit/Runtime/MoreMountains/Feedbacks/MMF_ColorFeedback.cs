#if GAMEKIT_MM_FEEDBACKS_INTEGRATION
using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Alven.GameKit.MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback lets you control the color of a target TMP over time
    /// </summary>
    [FeedbackHelp("This feedback lets you control the color of a target TMP over time.")]
    public abstract class MMF_ColorFeedback : MMF_Feedback
    {
        /// sets the inspector color for this feedback
#if UNITY_EDITOR
        public override Color FeedbackColor => MMFeedbacksInspectorColors.RendererColor;

        public override string RequiresSetupText =>
            "This feedback requires that a TargetTMPText be set to be able to work properly. You can set one below.";
#endif

        /// a static bool used to disable all feedbacks of this type at once
        public static bool FeedbackTypeAuthorized = true;

        public enum ColorModes
        {
            Instant,
            Gradient,
            Interpolate
        }

        /// the duration of this feedback is the duration of the color transition, or 0 if instant
        public override float FeedbackDuration
        {
            get => ColorMode == ColorModes.Instant ? 0f : ApplyTimeMultiplier(Duration);
            set => Duration = value;
        }

        public override bool HasAutomatedTargetAcquisition => true;

        [MMFInspectorGroup("Color", true, 16)]
        [Tooltip("the selected color mode :" +
                 "None : nothing will happen," +
                 "gradient : evaluates the color over time on that gradient, from left to right," +
                 "interpolate : lerps from the current color to the destination one ")]
        public ColorModes ColorMode = ColorModes.Interpolate;

        [Tooltip("how long the color of the text should change over time")]
        [MMFEnumCondition(nameof(ColorMode), (int)ColorModes.Interpolate, (int)ColorModes.Gradient)]
        public float Duration = 0.2f;

        [Tooltip("the color to apply")]
        [MMFEnumCondition(nameof(ColorMode), (int)ColorModes.Instant)]
        public Color InstantColor = Color.yellow;

        [Tooltip("the gradient to use to animate the color over time")]
        [MMFEnumCondition(nameof(ColorMode), (int)ColorModes.Gradient)]
        [GradientUsage(true)]
        public Gradient ColorGradient;

        [Tooltip("the destination color when in interpolate mode")]
        [MMFEnumCondition(nameof(ColorMode), (int)ColorModes.Interpolate)]
        public Color DestinationColor = Color.yellow;

        [Tooltip("the curve to use when interpolating towards the destination color")]
        [MMFEnumCondition(nameof(ColorMode), (int)ColorModes.Interpolate)]
        public AnimationCurve ColorCurve = new(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        [Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, " +
                 "it'll prevent any new Play until the current one is over")]
        public bool AllowAdditivePlays;

        protected Color _initialColor;
        protected Coroutine _coroutine;

        /// <summary>
        /// On init we store our initial color
        /// </summary>
        /// <param name="owner"></param>
        protected override void CustomInitialization(MMF_Player owner)
        {
            base.CustomInitialization(owner);

            if (EvaluateRequiresSetup()) return;
            _initialColor = GetColor();
        }

        /// <summary>
        /// On Play we change our text's color
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (!Active || !FeedbackTypeAuthorized || EvaluateRequiresSetup()) return;

            switch (ColorMode)
            {
                case ColorModes.Instant:
                    SetColor(InstantColor);
                    break;
                case ColorModes.Gradient:
                    if (!AllowAdditivePlays && _coroutine != null)
                    {
                        return;
                    }

                    _coroutine = Owner.StartCoroutine(ChangeColor());
                    break;
                case ColorModes.Interpolate:
                    if (!AllowAdditivePlays && _coroutine != null)
                    {
                        return;
                    }

                    _coroutine = Owner.StartCoroutine(ChangeColor());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Changes the color of the text over time
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ChangeColor()
        {
            float journey = NormalPlayDirection ? 0f : FeedbackDuration;
            IsPlaying = true;
            while (journey >= 0 && journey <= FeedbackDuration && FeedbackDuration > 0)
            {
                float remappedTime = MMFeedbacksHelpers.Remap(journey, 0f, FeedbackDuration, 0f, 1f);

                SetColor(remappedTime);

                journey += NormalPlayDirection ? FeedbackDeltaTime : -FeedbackDeltaTime;
                yield return null;
            }

            SetColor(FinalNormalizedTime);
            _coroutine = null;
            IsPlaying = false;
        }

        /// <summary>
        /// Stops the animation if needed
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (!Active || !FeedbackTypeAuthorized) return;

            base.CustomStopFeedback(position, feedbacksIntensity);
            IsPlaying = false;
            if (_coroutine == null) return;
            Owner.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        /// <summary>
        /// Applies the color change
        /// </summary>
        /// <param name="time"></param>
        protected virtual void SetColor(float time)
        {
            switch (ColorMode)
            {
                case ColorModes.Gradient:
                    SetColor(ColorGradient.Evaluate(time));
                    break;
                case ColorModes.Interpolate:
                {
                    float factor = ColorCurve.Evaluate(time);
                    SetColor(Color.LerpUnclamped(_initialColor, DestinationColor, factor));
                    break;
                }
                case ColorModes.Instant:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// On restore, we put our object back at its initial position
        /// </summary>
        protected override void CustomRestoreInitialValues()
        {
            if (!Active || !FeedbackTypeAuthorized) return;
            SetColor(_initialColor);
        }

        protected override void CustomReset()
        {
            if (!Active || !FeedbackTypeAuthorized) return;
            
            if (InCooldown) return;
            
            base.CustomReset();
            
            SetColor(_initialColor);
        }

        protected abstract void SetColor(Color color);
        protected abstract Color GetColor();
    }
}
#endif