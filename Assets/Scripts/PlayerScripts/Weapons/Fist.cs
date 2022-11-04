using System.Collections;
using UnityEngine;

public class Fist : Weapon
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackRange = 2;

    private Animator _animator;
    private AnimationHasher _animationHasher;
    
    public Fist()
    {
        AttackSpeed = 1f;
        Damage = 5;
    }

    protected override void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _animationHasher = GetComponentInParent<AnimationHasher>();
    }

    public override IEnumerator AttackRoutine(float direction)
    {
        _animator.StopPlayback();
        _animator.CrossFade(_animationHasher.AttackHash, 0);

        AttackTarget(direction);
        yield return null;
    }

    public override void GiveDamage(Enemy target)
    {
        target.ApplyDamage(Damage);
    }

    private void AttackTarget(float direction)
    {
        if (Physics.Raycast(transform.position, transform.right * direction, out RaycastHit hit, _attackRange,
                _enemyLayer))
        {
            if (hit.collider.TryGetComponent(out MinotaurEnemy enemy))
            {
                GiveDamage(enemy);
            }
        }
    }
}