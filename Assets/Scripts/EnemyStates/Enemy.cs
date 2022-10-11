using UnityEngine;
using UnityEngine.Events;

public class Enemy : EnemyBase
{
    [SerializeField] private CharacterData _dataEntity;
    [SerializeField] private DataEnemyFight _dataEnemyFight;
    [SerializeField] private PlayerEntity _target;

    public PlayerEntity Target => _target;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        EnemyObserver = GetComponent<EnemyObserve>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
