using System.Collections;
using UnityEngine;

public class Fist : WeaponBase
{
    [SerializeField] private LayerMask _enemyLayer;

    public override IEnumerator AttackRoutine(float direction)
    {
	    CanAttack = false;
        SetAttackAnimation();
        AttackTarget(direction);
        yield return new WaitForSeconds(AttackSpeed);
        CanAttack = true;
    }

    public override void GiveDamage(Enemy target)
    {
        target.ApplyDamage(Damage);
    }

    private void AttackTarget(float direction)
    {
        if (Physics.Raycast(transform.position, transform.right * direction, out RaycastHit hit, Range,
                _enemyLayer))
        {
            if (hit.collider.TryGetComponent(out MinotaurEnemy enemy))
            {
                GiveDamage(enemy);
            }
        }
    }
}