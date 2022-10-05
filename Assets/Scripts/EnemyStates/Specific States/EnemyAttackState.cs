using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(float lastAttackTime, int Damage)
    {
        _lastAttackTime = lastAttackTime;
    }

    private EnemyMeele _enemyMeele;
    private Animator _animator;

    private float _lastAttackTime;
    private readonly string _attackBoolName = "isAttack";



    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public void Attack(PlayerEntity target)
    {
        _animator.Play(_attackBoolName);
        target.ApplyDamage(_enemyMeele.Damage);
        _lastAttackTime = _enemyMeele.AttackSpeed;
    }
}