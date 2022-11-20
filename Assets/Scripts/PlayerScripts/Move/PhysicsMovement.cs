using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemReader), typeof(SurfaceInformant))]
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
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

	private bool _isGrounded;
	private bool _isGlide;

	public event UnityAction Glided;

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
		_groundChecker.GroundedStateSwitched += OnSwitchFallState;
		_surfaceInformant.Glides += OnGlide;
	}

	private void OnDisable()
	{
		_inputSystemReader.VerticalMoveButtonCanceled -= OnCancelHorizontalMove;
		_inputSystemReader.VerticalMoveButtonUsed -= OnHorizontalMove;
		_inputSystemReader.JumpButtonUsed -= OnJump;
		_inputSystemReader.JumpButtonCanceled -= OnStopJump;
		_groundChecker.GroundedStateSwitched -= OnSwitchFallState;
		_surfaceInformant.Glides -= OnGlide;
	}

	private void Start() =>
		StartCoroutine(FallRoutine());

	private void FixedUpdate() =>
		Move();

	private void OnSwitchFallState(bool isFall)
	{
		_isGrounded = isFall;

		if (_isGrounded == false)
			StartFallCoroutine();
	}

	private void OnCancelHorizontalMove() =>
		_movementDirection = new Vector2(0, _movementDirection.y);

	private void OnHorizontalMove(float direction) =>
		_movementDirection = new Vector2(direction, _movementDirection.y);

	private void OnJump()
	{
		if (_isGrounded == false)
			return;

		var velocity = _rigidbody2D.velocity;

		Vector2 jumpDirection = new Vector2(velocity.x, _jumpForce);
		velocity = jumpDirection;
		_inertiaDirection = velocity;
	}

	private void OnGlide(bool isGlide)
	{
		Glided?.Invoke();
		_isGlide = isGlide;
		StartFallCoroutine();
	}

	private void StartFallCoroutine()
	{
		if (_currentFallCoroutine != null)
		{
			StopCoroutine(_currentFallCoroutine);
			_currentFallCoroutine = null;
		}

		_currentFallCoroutine = StartCoroutine(FallRoutine());
	}

	private void OnStopJump()
	{
		const float ZeroVerticalVelocity = 0;
		_inertiaDirection = new Vector2(_rigidbody2D.velocity.x, ZeroVerticalVelocity);
	}

	private IEnumerator FallRoutine()
	{
		while (_isGrounded == false || _isGlide == true)
		{
			_inertiaDirection.y -= _gravityModifier * -Physics2D.gravity.y * Time.deltaTime;
			_inertiaDirection.x = _movementDirection.x;
			yield return null;
		}

		_inertiaDirection = Vector2.zero;
	}

	private void Move()
	{
		Vector2 normalizedDirection = _surfaceInformant.GetProjection(_movementDirection);

		_offset = _inertiaDirection + (normalizedDirection * _moveSpeed);
		_offset.y = Mathf.Clamp(_offset.y, -_verticalVelocityLimit, int.MaxValue);

		_rigidbody2D.position += _offset * Time.deltaTime;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + _offset);
	}
}