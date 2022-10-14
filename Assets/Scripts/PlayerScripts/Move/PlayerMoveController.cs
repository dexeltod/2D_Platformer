using UnityEngine;

[RequireComponent(typeof(InputSystemReader))]

public class PlayerMoveController : PhysicsMovement
{
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private float _speedModifier = 5;
    [SerializeField] private float _maxJumpHight = 9.2f;
    [SerializeField] private float _jumpSpeedSlowdown;

    [Header("Debug")]
    [SerializeField] private bool _debugGizmos;

    protected InputSystemReader InputSystemReader;
    private Vector2 _moveDirection;

    public void OnValidate()
    {
        if (_minGroundNormalY <= 0 || _minGroundNormalY >= 1)
            _minGroundNormalY = 0;

        if (_jumpSpeedSlowdown > 1 || _jumpSpeedSlowdown < 0)
            _jumpSpeedSlowdown = 0;

        if (_speedModifier < 0)
            _speedModifier = 0;
    }

    private void Awake()
    {
        InputSystemReader = GetComponent<InputSystemReader>();
    }
    private void OnEnable()
    {
        InputSystemReader.JumpButtonUsed += (directon) => SetJumpState(directon);
        InputSystemReader.VerticalMoveButtonUsed += (directon) => SetMoveHorizontalDirection(directon);
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        ContactFilter.useTriggers = false;
        ContactFilter.useLayerMask = true;
    }

    private void Update()
    {
        ComputeVelocity();
    }

    public void SetJumpDir(float direction)
    {
        _moveDirection.y = direction;
    }

    public void SetMoveHorizontalDirection(float direction)
    {
        _moveDirection.x = direction;
    }

    protected void ComputeVelocity()
    {
        Vector2 moveNormilized = Vector2.zero;
        moveNormilized.x = _moveDirection.x;
        TargetVelocity = moveNormilized * _speedModifier;
    }

    private void SetJumpState(float direction)
    {
        if (direction == 1 && IsGrounded == true)
        {
            IsJump = true;
            Velocity.y = _jumpTakeOffSpeed;
        }
        else if (Velocity.y >= _maxJumpHight && IsJump)
        {
            Velocity.y = _maxJumpHight;
        }
        else if (!IsGrounded && _moveDirection.y == 0 && IsJump == true)
        {
            float velocityDecreaser = 0.5f;

            IsJump = false;
            Velocity.y *= velocityDecreaser;
        }
    }

    private void OnDrawGizmos()
    {
        if (_debugGizmos == true)
        {
            Gizmos.DrawCube(transform.position, new Vector2(0.1f, 0.1f));
        }
    }
}
