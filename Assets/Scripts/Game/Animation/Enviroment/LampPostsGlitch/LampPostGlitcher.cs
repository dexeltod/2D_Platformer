using System.Collections;
using UnityEngine;

namespace Game.Animation.Enviroment.LampPostsGlitch
{
    [RequireComponent(typeof(Animator))]

    public class LampPostGlitcher : MonoBehaviour
    {
        [SerializeField] private bool _isWork = true;
        [SerializeField] private float _minStartDelay = 10;
        [SerializeField] private float _maxStartDelay = 20;
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            StartCoroutine(EnableLight());
        }

        private IEnumerator EnableLight()
        {
            float startDelay = Random.Range(_minStartDelay, _minStartDelay);
            WaitForSeconds workTime = new(startDelay);

            float maxGlitchDelay = 1f;
            float glitchDelay = Random.Range(0, maxGlitchDelay);
            WaitForSeconds glitchTime = new(glitchDelay);

            while (_isWork)
            {
                yield return workTime;
                _animator.enabled = true;
                yield return glitchTime;
                _animator.enabled = false;
            }
        }
    }
}
