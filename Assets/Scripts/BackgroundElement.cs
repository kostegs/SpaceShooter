using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]    
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]
        [SerializeField] private float _parallaxPower;

        [SerializeField] private float _textureScale;

        private Material _quadMaterial;
        private Vector2 _initialOffset;

        private static int _count;

        private void Start()
        {
            _count++;
            _quadMaterial = GetComponent<MeshRenderer>().material;
            
            // случайная точка в рамках единичной окружности
            _initialOffset = Random.insideUnitSphere;            

            _quadMaterial.mainTextureScale = Vector2.one * _textureScale;            
        }

        private void Update()
        {
            Vector2 offset = _initialOffset;

            offset.x += transform.position.x / transform.localScale.x / _parallaxPower;
            offset.y += transform.position.y / transform.localScale.y / _parallaxPower;

            _quadMaterial.mainTextureOffset = offset;
        }

    }
}
