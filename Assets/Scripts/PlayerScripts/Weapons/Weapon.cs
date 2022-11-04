using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private Animator _animator;
    private AnimationHasher _animations;

    public bool CanAttack { get; protected set; }
    public int Damage { get; protected set; }
    public float AttackSpeed { get; protected set; }

    public abstract IEnumerator AttackRoutine(float direction);
    public abstract void GiveDamage(Enemy target);

    public void Initialize(Animator animator, AnimationHasher hasher)
    {
        _animator = animator;
        _animations = hasher;
    }

    protected virtual void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        Debug.Log(_animator);
    }

    protected void SetAttackAnimation()
    {
        _animator.CrossFade(_animations.AttackHash, 0);
    }
}