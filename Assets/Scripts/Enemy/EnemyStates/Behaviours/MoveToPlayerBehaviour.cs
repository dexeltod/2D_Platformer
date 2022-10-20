using UnityEngine;
using UnityEngine.Events;

public class MoveToPlayerBehaviour : MonoBehaviour
{
    public event UnityAction PlayerReached;

    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private DataEnemy _dataEnemy;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = _playerCharacter.transform.position - transform.position;
        _rigidbody2D.position += direction.normalized * _dataEnemy.MoveSpeed;
        CheckDistanceBetweenPlayer();
    }

    private void CheckDistanceBetweenPlayer()
    {
        float distance = Vector2.Distance(transform.position, _playerCharacter.transform.position);
        
        if (distance <= _dataEnemy.AttackRange)
        {
            PlayerReached?.Invoke();
        }
    }
}