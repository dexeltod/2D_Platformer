using UnityEngine;

public class PlayerMoveController : PhysicsMovement
{
    private Vector2 _moveDirection;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private float _speedModifier = 5;
    [SerializeField] private float _maxJumpHight = 9.2f;
    [SerializeField] private float _jumpSpeedSlowdown;

    [Header("Debug")]

    [SerializeField] bool _debugGizmos;
      

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

    public void SetJumpDir()
    {
        _moveDirection.y = _inputSystemReader.ButtonMoveValue;
    }

    public void SetMoveHorizontalDirection()
    {
        _moveDirection.x = _inputSystemReader.ButtonMoveValue;
    }

    private void Start()
    {
        _inputSystemReader = GetComponent<InputSystemReader>();
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
        if (_inputSystemReader.ButtonJumpValue == 1 && _isGrounded == true)
        {
            _isJump = true;
            _velocity.y = _jumpTakeOffSpeed;
        }
        else if (_velocity.y >= _maxJumpHight && _isJump)
        {
            _velocity.y = _maxJumpHight;
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
