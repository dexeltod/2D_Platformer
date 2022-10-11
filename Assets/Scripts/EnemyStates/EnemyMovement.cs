using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyObserve))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private CharacterData _verticalSpeed;

    private Rigidbody2D _rigidbody;
    private EnemyObserve _observer;
    private Vector2 _velocity;

    public void DecreaseVerticalVelocity()
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

    private void OnEnable()
    {
        _velocity = new Vector2(_verticalSpeed.MoveSpeed, transform.position.y);
    }

    private void VerticalMovemet()
    {
        float verticalVelocity = _verticalSpeed.MoveSpeed * _observer.FacingDirection;
        bool canMove = _observer.CheckLedge() || !_observer.CheckColliderHorizontal();

        if (canMove)
        {
            _rigidbody.velocity = new Vector2(verticalVelocity, _rigidbody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        VerticalMovemet();
    }

    private void OnDisable() => DecreaseVerticalVelocity();
}
