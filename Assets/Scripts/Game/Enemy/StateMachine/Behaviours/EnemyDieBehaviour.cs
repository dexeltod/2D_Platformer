using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationHasher), typeof(Animator))]

public class EnemyDieBehaviour : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private CapsuleCollider2D _collider;
    private Animator _animator;
    private AnimationHasher _animationHasher;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _animationHasher = GetComponent<AnimationHasher>();
    }

    private void OnEnable()
    {
        _collider.enabled = false;
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