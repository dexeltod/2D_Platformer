using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHasher), typeof(Animator), typeof(Rigidbody2D))]
public class MoveToPlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DataEnemy _dataEnemy;

    public event UnityAction PlayerReached;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private Animator _animator;
    private AnimationHasher _animationHasher;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationHasher = GetComponent<AnimationHasher>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _animator.StopPlayback();
        _animator.CrossFade(_animationHasher.RunHash, 0);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }

    private void FixedUpdate()
    {
        Vector2 direction = _player.transform.position - transform.position;
        _rigidbody2D.position += direction.normalized * _dataEnemy.MoveSpeed;
        CheckDistanceBetweenPlayer();
    }

    private void CheckDistanceBetweenPlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance <= _dataEnemy.AttackRange)
            PlayerReached?.Invoke();
    }
}