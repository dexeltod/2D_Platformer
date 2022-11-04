using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHasher), typeof(Animator))]
public class EnemyAttackBehaviour : MonoBehaviour
{
    [SerializeField] private DataEnemy _enemyData;
    [SerializeField] private PlayerHealth _playerHealth;

    public event UnityAction PlayerDied;
    public event UnityAction PlayerOutOfRangeAttack;

    private Animator _animator;
    private AnimationHasher _animationHasher;
    private Coroutine _currentCoroutine;
    private bool _canAttack = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationHasher = GetComponent<AnimationHasher>();
    }

    private void OnEnable()
    {
        _playerHealth.Died += OnPlayerHealthDie;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(AttackPlayer());
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
        _playerHealth.Died -= OnPlayerHealthDie;

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }

    public void Initialize(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }
    
    private IEnumerator AttackPlayer()
    {
        SetAnimatorSettings();
        var waitingTime = new WaitForSeconds(GetAnimationSpeed());

        while (_canAttack)
        {
            if (CantReachPlayer())
                PlayerOutOfRangeAttack?.Invoke();

            _playerHealth.ApplyDamage(_enemyData.Damage);
            yield return waitingTime;
        }
    }

    private void SetAnimatorSettings()
    {
        float transitionDuration = 0;

        _animator.StopPlayback();
        _animator.CrossFade(_animationHasher.AttackHash, transitionDuration);
        _animator.SetFloat(_animationHasher.AttackSpeedHash, _enemyData.AttackSpeed);
    }

    private float GetAnimationSpeed()
    {
        int currentLayer = 0;
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(currentLayer);
        return stateInfo.length;
    }

    private float GetDistanceBetweenPlayer() =>
        Vector2.Distance(transform.position, _playerHealth.transform.position);

    private bool CantReachPlayer() => GetDistanceBetweenPlayer() > _enemyData.AttackRange;

    private void OnPlayerHealthDie()
    {
        PlayerDied?.Invoke();
        _canAttack = false;
    }
}