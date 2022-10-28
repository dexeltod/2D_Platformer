using UnityEngine;

[RequireComponent(typeof(InputSystemReader))]
[RequireComponent(typeof(Rigidbody2D))]

public class PhysicsMovementTest : MonoBehaviour
{
    [SerializeField] private float _gravityModifier = 1f;
    
    private Rigidbody2D _rigidbody2D;
    private InputSystemReader _inputSystemReader;
    private Vector2 _moveDirection;
    private Vector2 _velocity;

    private void Awake()
    {
        _inputSystemReader = GetComponent<InputSystemReader>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputSystemReader.VerticalMoveButtonUsed += OnHorizontalMove;
        _inputSystemReader.JumpButtonUsed += OnJump;
    }

    private void OnDisable()
    {
        _inputSystemReader.VerticalMoveButtonUsed -= OnHorizontalMove;
        _inputSystemReader.JumpButtonUsed -= OnJump;
    }

    private void OnHorizontalMove(float direction)
    {
        _moveDirection = new(direction, _moveDirection.y);
    }

    private void OnJump(Vector2 direction)
    {
        _moveDirection = new(_moveDirection.x, direction.x);
    }

    private void Move()
    {
        _rigidbody2D.gravityScale = _gravityModifier;

        _velocity += _rigidbody2D.gravityScale * Time.deltaTime * Physics2D.gravity;
        _velocity.x = _moveDirection.x;
        
        _rigidbody2D.position += _velocity;
    }
    
    private void FixedUpdate()
    {
        Move();
    }
}