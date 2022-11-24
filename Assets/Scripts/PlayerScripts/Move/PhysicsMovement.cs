using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemReader), typeof(SurfaceInformant))]
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
	[SerializeField] private bool _isDebug;

	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _jumpForce = 3f;
	[SerializeField] private float _gravityModifier = 1f;
	[SerializeField] private float _verticalVelocityLimit = -5f;

	private GroundChecker _groundChecker;
	private SurfaceInformant _surfaceInformant;
	private InputSystemReader _inputSystemReader;
	private Rigidbody2D _rigidbody2D;
	private Coroutine _currentFallCoroutine;

	private Vector2 _movementDirection;
	private Vector2 _inertiaDirection;
	private Vector2 _offset;

	private bool _isGlide = false;
	private bool _lastIsRotate = false;
	private bool _isRotate = true;

	public bool IsGrounded { get; private set; }
	public Vector2 MovementDirection => _movementDirection;
	public Vector2 Offset => _offset;

	private int _currentJumpStopCount = 0;
	private int _maxJumpStopCount;

	public event UnityAction Glided;
	public event UnityAction Fallen;
	public event UnityAction Grounded;
	public event UnityAction Jumped;
	public event UnityAction<bool> Rotated;

	private void Awake()
	{
		_groundChecker = GetComponent<GroundChecker>();
		_surfaceInformant = GetComponent<SurfaceInformant>();
		_inputSystemReader = GetComponent<InputSystemReader>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		_inputSystemReader.VerticalMoveButtonCanceled += OnCancelHorizontalMove;
		_inputSystemReader.VerticalMoveButtonUsed += OnHorizontalMove;
		_inputSystemReader.JumpButtonUsed += OnJump;
		_inputSystemReader.JumpButtonCanceled += OnStopJump;
		_groundChecker.GroundedStateSwitched += OnSwitchGroundState;
		_surfaceInformant.Glides += OnGlide;
	}

	private void OnDisable()
	{
		_inputSystemReader.VerticalMoveButtonCanceled -= OnCancelHorizontalMove;
		_inputSystemReader.VerticalMoveButtonUsed -= OnHorizontalMove;
		_inputSystemReader.JumpButtonUsed -= OnJump;
		_inputSystemReader.JumpButtonCanceled -= OnStopJump;
		_groundChecker.GroundedStateSwitched -= OnSwitchGroundState;
		_surfaceInformant.Glides -= OnGlide;
	}

	private void Start() =>
		StartCoroutine(FallRoutine());

	private void FixedUpdate() =>
		Move();

	private void Move()
	{
		Vector2 normalizedDirection = _surfaceInformant.GetProjection(_movementDirection);

		_offset = _inertiaDirection + (normalizedDirection * _moveSpeed);
		_offset.y = Mathf.Clamp(_offset.y, -_verticalVelocityLimit, int.MaxValue);

		CheckDirection();
		_rigidbody2D.position += _offset * Time.deltaTime;
	}

	private void OnCancelHorizontalMove() =>
		_movementDirection = new Vector2(0, _movementDirection.y);

	private void OnHorizontalMove(float direction) =>
		_movementDirection = new Vector2(direction, _movementDirection.y);

	private void OnJump()
	{
		if (IsGrounded == false)
			return;

		var velocity = _rigidbody2D.velocity;
		Vector2 jumpDirection = new Vector2(velocity.x, _jumpForce);
		velocity = jumpDirection;
		_inertiaDirection = velocity;

		_maxJumpStopCount++;
		StartCoroutine(JumpRoutine());
	}

	private void OnSwitchGroundState(bool isFall)
	{
		IsGrounded = isFall;

		if (IsGrounded == false || _offset.y < 0) StartFallCoroutine();
	}

	private void OnGlide(bool isGlide)
	{
		Glided?.Invoke();
		_isGlide = isGlide;
		StartFallCoroutine();
	}

	private void CheckDirection()
	{
		if (_offset.x > 0.01f)
			_isRotate = false;
		else if (_offset.x < -0.01f)
			_isRotate = true;

		if (_lastIsRotate == _isRotate)
			return;

		_lastIsRotate = _isRotate;
		Rotated?.Invoke(_lastIsRotate);
	}

	private void StartFallCoroutine()
	{
		if (_currentFallCoroutine != null)
		{
			StopCoroutine(_currentFallCoroutine);
			_currentFallCoroutine = null;
		}

		if (_isGlide == false)
			Fallen?.Invoke();

		_currentFallCoroutine = StartCoroutine(FallRoutine());
	}

	private void OnStopJump()
	{
		const float VerticalVelocityZero = 0;

		if (_currentJumpStopCount >= _maxJumpStopCount)
			return;

		_inertiaDirection = new Vector2(_rigidbody2D.velocity.x, VerticalVelocityZero);
		_currentJumpStopCount++;
	}

	private IEnumerator FallRoutine()
	{
		while (IsGrounded == false || _isGlide == true)
		{
			_inertiaDirection.y -= _gravityModifier * -Physics2D.gravity.y * Time.deltaTime;
			_inertiaDirection.x = _movementDirection.x;
			yield return null;
		}

		IsGrounded = true;
		Grounded?.Invoke();
		_currentJumpStopCount = 0;
		_maxJumpStopCount = 0;
		_inertiaDirection = Vector2.zero;
	}

	private IEnumerator JumpRoutine()
	{
		Jumped?.Invoke();

		while (IsGrounded == false)
			yield return null;
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