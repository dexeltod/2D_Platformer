using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationHasher), typeof(Animator))]

public class EnemyDieBehaviour : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private Animator _animator;
    private AnimationHasher _animationHasher;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationHasher = GetComponent<AnimationHasher>();
    }

    private void OnEnable()
    {
        float transitionDuration = 0;
        _animator.StopPlayback();
        _animator.CrossFade(_animationHasher.DieHash, transitionDuration);
        
        StartCoroutine(StartTimerToDestroy());
    }

    private IEnumerator StartTimerToDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }
}