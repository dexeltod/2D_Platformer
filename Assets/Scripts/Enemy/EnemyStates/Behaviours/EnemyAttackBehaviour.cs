using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackBehaviour : MonoBehaviour
{
    public event UnityAction PlayerDied;
    public event UnityAction PlayerOutOfRangeAttack;

    [SerializeField] private DataEnemy _enemyData;
    [SerializeField] private PlayerHealth _player;

    private Coroutine _currentCoroutine;
    private bool _canAttack = true;

    private void OnEnable()
    {
        _player.Died += OnPlayerDie;
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(AttackPlayer());
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDie;
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator AttackPlayer()
    {
        var attackDelay = new WaitForSeconds(_enemyData.AttackDelay);

        while (_canAttack)
        {
            if(CantReachPlayer())
                PlayerOutOfRangeAttack?.Invoke();
                
            _player.ApplyDamage(_enemyData.Damage);
            yield return attackDelay;
        }
    }

    private float GetDistanceBetweenPlayer()
    {
        return Vector2.Distance(transform.position, _player.transform.position);
    }

    private bool CantReachPlayer() => GetDistanceBetweenPlayer() > _enemyData.AttackRange;
        
    
    private void OnPlayerDie()
    {
        PlayerDied?.Invoke();
        _canAttack = false;
    }
}