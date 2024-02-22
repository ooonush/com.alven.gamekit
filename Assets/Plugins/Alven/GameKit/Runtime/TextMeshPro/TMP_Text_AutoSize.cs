#if GAMEKIT_TEXTMESHPRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace Alven.GameKit.TextMeshPro
{
    [ExecuteAlways]
    public class TMP_Text_AutoSize : MonoBehaviour
    {
        public bool Height;
        public bool Width;
        [SerializeField] private TMP_Text _text;
        private RectTransform TextRectTransform => _text.rectTransform;

        private void LateUpdate()
        {
            if (!_text) return;
            
            if (Height)
            {
                if (Mathf.Abs(TextRectTransform.rect.height - _text.preferredHeight) > Mathf.Epsilon)
                {
                    TextRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _text.preferredHeight);
                }
            }
            
            if (Width)
            {
                if (Mathf.Abs(TextRectTransform.rect.width - _text.preferredWidth) > Mathf.Epsilon)
                {
                    TextRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _text.preferredWidth);
                }
            }
        }
    }
}
#endif