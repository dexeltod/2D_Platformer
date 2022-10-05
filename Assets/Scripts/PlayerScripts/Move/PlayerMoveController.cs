using UnityEngine;

public class PlayerMoveController : PhysicsMovement
{
    private Vector2 _moveDirection;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private float _speedModifier = 5;
    [SerializeField] private float _maxTakeOffHight = 9.2f;
    [SerializeField] private float _jumpSpeedSlowdown;

    [Header("Debug")]

    [SerializeField] bool _debugGizmos;

    public void SetJumpDir(float jumpDir)
    {
        _moveDirection.y = jumpDir;
    }

    public void SetMoveHorizontalDirection(Vector2 moveDir)
    {
        _moveDirection.x = moveDir.x;
    }

    public void OnValidate()
    {
        if (_minGroundNormalY <= 0 || _minGroundNormalY >= 1)
        {
            _minGroundNormalY = 0;
        }

        if (_jumpSpeedSlowdown > 1 || _jumpSpeedSlowdown < 0)
            _jumpSpeedSlowdown = 0;

        if (_speedModifier < 0)
            _speedModifier = 0;
    }

    private void Start()
    {
        _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _contactFilter.useTriggers = false;
        _contactFilter.useLayerMask = true;
    }

    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ComputeVelocity();
    }

    protected void ComputeVelocity()
    {
        Vector2 moveNormilized = Vector2.zero;
        moveNormilized.x = _moveDirection.x;
        SetJumpState();
        _targetVelocity = moveNormilized * _speedModifier;
    }

    private void SetJumpState()
    {
        if (_moveDirection.y == 1 && _isGrounded == true)
        {
            _isJump = true;
            _velocity.y = _jumpTakeOffSpeed;
        }
        else if (_velocity.y >= _maxTakeOffHight && _isJump)
        {
            _velocity.y = _maxTakeOffHight;
        }
        else if (!_isGrounded && _moveDirection.y == 0 && _isJump == true)
        {
            _isJump = false;
            _velocity.y *= 0.5f;
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
