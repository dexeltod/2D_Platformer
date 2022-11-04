using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyObserve))]
public class EnemyPatrolBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterData _verticalSpeed;

    private Rigidbody2D _rigidbody;
    private EnemyObserve _observer;

    private void DecreaseVerticalVelocity()
    {
        float minVelocity = 0;
        float minRigidbodyVelocity = _rigidbody.velocity.x * minVelocity;

        _rigidbody.velocity = new Vector2(minRigidbodyVelocity, _rigidbody.velocity.y);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _observer = GetComponent<EnemyObserve>();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void OnDisable() => DecreaseVerticalVelocity();

    private void Patrol()
    {
        float verticalVelocity = _verticalSpeed.MoveSpeed * _observer.FacingDirection;
        bool canMove = _observer.CheckLedge() || !_observer.CheckColliderHorizontal();

        if (canMove)
            _rigidbody.velocity = new Vector2(verticalVelocity, _rigidbody.velocity.y);
    }
}