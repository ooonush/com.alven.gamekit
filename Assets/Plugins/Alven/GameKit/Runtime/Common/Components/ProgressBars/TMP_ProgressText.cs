#if GAMEKIT_TEXTMESHPRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace Alven.GameKit.Common.ProgressBars
{
    [AddComponentMenu("GameKit/ProgressBars/ProgressText - TextMeshPro")]
    public class TMP_ProgressText : MonoBehaviour
    {
        [SerializeField] private ProgressBehaviour _progress;
        [SerializeField] private TMP_Text _text;
        public string TextFormat = "{000}";
        public string TextPrefix;
        public string TextSuffix;
        public float TextValueMultiplier = 1f;
        public bool DisplayTotal;
        public string TotalSeparator = "/";

        private void OnEnable()
        {
            _progress.OnChanged.AddListener(OnChanged);
            UpdateText();
        }

        private void OnDisable()
        {
            _progress.OnChanged.RemoveListener(OnChanged);
        }

        private void OnChanged(float change)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            string text = TextPrefix + (_progress.Value * TextValueMultiplier).ToString(TextFormat);
            if (DisplayTotal)
            {
                text += TotalSeparator + TextValueMultiplier.ToString(TextFormat);
            }
            text += TextSuffix;

            _text.text = text;
        }
    }
}
#endif