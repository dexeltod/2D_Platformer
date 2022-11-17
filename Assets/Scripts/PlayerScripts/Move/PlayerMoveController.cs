using UnityEngine;

[RequireComponent(typeof(InputSystemReader))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMoveController : PhysicsMovementOld
{
	[SerializeField] private float _speedModifier = 5;
	[SerializeField] private float _jumpTakeOffSpeed = 7;
	[SerializeField] private float _maxJump = 9.2f;
	[SerializeField] private float _jumpVelocityDecrease = 0.5f;
	[SerializeField] private float _jumpSpeedSlowdown;

	[Header("Debug")] [SerializeField] private bool _debugGizmos;

	private InputSystemReader _inputSystemReader;
	private Vector2 _moveDirection;

	public void OnValidate()
	{
		if (_jumpVelocityDecrease < 0 || _jumpSpeedSlowdown >= 1)
			_jumpVelocityDecrease = 0.5f;

		if (MinGroundNormalY <= 0 || MinGroundNormalY >= 1)
			MinGroundNormalY = 0;

		if (_jumpSpeedSlowdown > 1 || _jumpSpeedSlowdown < 0)
			_jumpSpeedSlowdown = 0;

		if (_speedModifier < 0)
			_speedModifier = 0;
	}

	private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();
		_inputSystemReader = GetComponent<InputSystemReader>();
	}

	private void OnEnable()
	{
		_inputSystemReader.JumpButtonUsed += SetJumpState;
		_inputSystemReader.VerticalMoveButtonUsed += SetMoveHorizontalDirection;
	}

	private void Start()
	{
		ContactFilter.useLayerMask = true;
	}

	private void Update()
	{
		ComputeVelocity();
	}

	private void OnDisable()
	{
		_inputSystemReader.JumpButtonUsed -= SetJumpState;
		_inputSystemReader.VerticalMoveButtonUsed -= SetMoveHorizontalDirection;
	}

	public void SetJumpDir(float direction)
	{
		_moveDirection.y = direction;
	}

	private void SetMoveHorizontalDirection(float direction) =>
		_moveDirection.x = direction;

	private void ComputeVelocity()
	{
		Vector2 moveNormalized = Vector2.zero;
		moveNormalized.x = _moveDirection.x;
		TargetVelocity = moveNormalized * _speedModifier;
	}

	private void SetJumpState(float direction)
	{
		if (direction == 1f && IsGrounded == true)
		{
			IsJump = true;
			Velocity.y = _jumpTakeOffSpeed;
		}
		else if (Velocity.y >= _maxJump && IsJump)
		{
			Velocity.y = _maxJump;
		}
		else if (IsGrounded == false && _moveDirection.y == 0 && IsJump == true)
		{
			float velocityDecrease = 0.5f;

			IsJump = false;
			Velocity.y *= velocityDecrease;
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