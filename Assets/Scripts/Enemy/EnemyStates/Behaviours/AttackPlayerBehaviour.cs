using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackPlayerBehaviour : MonoBehaviour
{
    public event UnityAction PlayerDied;

    [SerializeField] private DataEnemy _enemyData;
    [SerializeField] private PlayerCharacter _playerCharacter;

    private Coroutine _currentCoroutine;
    private bool _canAttack = true;

    private void OnEnable()
    {
        _playerCharacter.Died += OnPlayerDie;
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(AttackPlayer());
    }

    private void OnDisable()
    {
        _playerCharacter.Died -= OnPlayerDie;
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator AttackPlayer()
    {
        var attackDelay = new WaitForSeconds(_enemyData.AttackDelay);

        while (_canAttack)
        {
            _playerCharacter.ApplyDamage(_enemyData.Damage);
            yield return attackDelay;
        }
    }

    private void OnPlayerDie()
    {
        PlayerDied?.Invoke();
        _canAttack = false;
    }
}