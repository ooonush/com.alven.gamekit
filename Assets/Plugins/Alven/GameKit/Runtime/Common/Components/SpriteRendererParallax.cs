using UnityEngine;

namespace Alven.GameKit.Common
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererParallax : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [Range(0.0001f, 1)]
        [SerializeField] private float _parallaxPercentX = 1;
        [Range(0.0001f, 1)]
        [SerializeField] private float _parallaxPercentY = 1;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            Vector3 cameraPosition = _camera.transform.position;
            float offsetX = cameraPosition.x * (1 - _parallaxPercentX);
            Material material = _renderer.material;
            float offsetY = cameraPosition.y * (1 - _parallaxPercentY);
            material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
}