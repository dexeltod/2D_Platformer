using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SurfaceInformant))]
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
	[SerializeField] private bool _isDebug;

	[SerializeField] private Transform _feetPosition;
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _jumpForce = 3f;
	[SerializeField] private float _verticalVelocityLimit = -5f;

	private GroundChecker _groundChecker;
	private SurfaceInformant _surfaceInformant;
	private Rigidbody2D _rigidbody2D;
	private Coroutine _currentFallCoroutine;
	private IInputService _inputService;

	private Vector2 _movementDirection;
	private Vector2 _inertiaDirection;
	private Vector2 _offset;

	private bool _isRotate = true;
	private bool _lastIsRotate;
	private bool _isCanMove;
	private bool _isGlide;
	private bool _isRunning;
	private bool _isRunningLast;
	private bool _isFall;
	private bool _lastIsFall;

	private int _currentJumpStopCount;
	private int _maxJumpStopCount;

	public Transform FeetPosition => _feetPosition;
	public bool IsGrounded { get; private set; }
	public Vector2 MovementDirection => _movementDirection;
	public Vector2 Offset => _offset;

	public event UnityAction Glided;
	public event UnityAction<bool> Fallen;
	public event UnityAction<bool> Running;
	public event UnityAction<bool> Rotating;

	private void Awake()
	{
		_groundChecker = GetComponent<GroundChecker>();
		_surfaceInformant = GetComponent<SurfaceInformant>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		_groundChecker.GroundedStateSwitched += OnSwitchGroundState;
		_surfaceInformant.GlideStateSwitched += OnGlideStateSwitched;
		_surfaceInformant.Moves += OnCanMoveChange;
	}

	private void OnDisable()
	{
		_groundChecker.GroundedStateSwitched -= OnSwitchGroundState;
		_surfaceInformant.GlideStateSwitched -= OnGlideStateSwitched;
		_surfaceInformant.Moves -= OnCanMoveChange;
	}

	private void FixedUpdate() =>
		Move();

	public void SetMoveDirection(float direction)
	{
		_movementDirection = new Vector2(direction, 0);
	}

	public void Jump()
	{
		if (IsGrounded == false)
			return;

		_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
		_maxJumpStopCount++;
	}

	private void OnSwitchGroundState(bool isGrounded)
	{
		IsGrounded = isGrounded;
	}

	private void OnCanMoveChange(bool canMove) => _isCanMove = canMove;

	private void OnGlideStateSwitched(bool isGlide)
	{
		Glided.Invoke();
		_isGlide = isGlide;
	}

	private void Move()
	{
		Vector2 normalizedDirection = _surfaceInformant.GetProjectionAlongNormal(_movementDirection);

		_offset = normalizedDirection * _moveSpeed;

		if (IsGrounded == false)
			_offset.y += -_rigidbody2D.gravityScale;
		
		_offset.y = Mathf.Clamp(_offset.y, -_verticalVelocityLimit, int.MaxValue);

		CheckFalling();
		CheckDirection();
		CheckRunning();
		_rigidbody2D.velocity = _offset;
	}

	private void CheckFalling()
	{
		if (IsGrounded == false && _rigidbody2D.velocity.y < 0)
		{
			
			if(_lastIsFall == _isFall)
				return;

			_lastIsFall = _isFall;
			Debug.Log("isFall");
			_isFall = true;
			Fallen?.Invoke(_isFall);
		}
	}

	private void CheckRunning()
	{
		_isRunning = IsGrounded == true && _isCanMove == true && _movementDirection.x != 0;

		if (_isRunning == _isRunningLast)
			return;

		_isRunningLast = _isRunning;

		Running.Invoke(_isRunning);
	}

	private void CheckDirection()
	{
		if (_isGlide == true || _isCanMove == false)
			return;

		if (_offset.x > 0.01f)
			_isRotate = false;
		else if (_offset.x < -0.01f)
			_isRotate = true;

		if (_lastIsRotate == _isRotate)
			return;

		_lastIsRotate = _isRotate;
		Rotating?.Invoke(_lastIsRotate);
	}

	private void OnDrawGizmos()
	{
		if (_isDebug == true)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(transform.position, (Vector2)transform.position + _offset);
		}
	}
}