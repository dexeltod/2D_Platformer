using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private DataEntity _dataEntity;
    [SerializeField] private DataEnemyFight _dataEnemyFight;
    [SerializeField] private PlayerEntity _target;

    private int _health;

    public PlayerEntity Target => _target;
    public event UnityAction Dying;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
            Destroy(gameObject);
    }
}
