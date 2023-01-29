using System.Collections;
using UnityEngine;

namespace Game.Enemy.VisualEffects
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float _colorChangingTime;

        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private Color _hitColor;
        private Coroutine _currentColorCoroutine;
        private Enemy _enemy;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable() =>
            _enemy.WasHit += OnColorChange;

        private void OnDisable() =>
            _enemy.WasHit -= OnColorChange;

        private void OnColorChange()
        {
            if (_currentColorCoroutine != null) 
                StopCoroutine(_currentColorCoroutine);

            _currentColorCoroutine = StartCoroutine(ChangeColorRoutine());
        }

        private IEnumerator ChangeColorRoutine()
        {
            _spriteRenderer.material = _hitMaterial;
            yield return new WaitForSeconds(_colorChangingTime);
            _spriteRenderer.material = _originalMaterial;
            _currentColorCoroutine = null;
        }
    }
}