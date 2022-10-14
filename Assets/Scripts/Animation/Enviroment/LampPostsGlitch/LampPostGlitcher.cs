using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class LampPostGlitcher : MonoBehaviour
{
    [SerializeField] private bool _isWork = true;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(EnableLight());
    }

    private IEnumerator EnableLight()
    {
        float startDelay = Random.Range(10, 20f);
        WaitForSeconds workTime = new(startDelay);

        float glitchDelay = Random.Range(0, 1f);
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
