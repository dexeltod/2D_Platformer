using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(InputSystemReader), typeof(SurfaceInformant))]
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
	private const float ShellRadius = 0.01f;

	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private Vector2 _groundCheckPosition;
	[SerializeField] private Vector2 _groundCheckSize;
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _jumpForce = 3f;
	[SerializeField] private float _gravityModifier = 1f;
	[SerializeField] private float _maxFallSpeed = -5f;

	private SurfaceInformant _surfaceInformant;
	private Rigidbody2D _rigidbody2D;
	private InputSystemReader _inputSystemReader;
	private Vector2 _movementDirection;
	private Vector2 _inertiaDirection;
	private Vector2 _offset;
	private bool _isGrounded;

	private void Awake()
	{
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
	}

	private void OnDisable()
	{
		_inputSystemReader.VerticalMoveButtonCanceled -= OnCancelHorizontalMove;
		_inputSystemReader.VerticalMoveButtonUsed -= OnHorizontalMove;
		_inputSystemReader.JumpButtonUsed -= OnJump;
		_inputSystemReader.JumpButtonCanceled -= OnStopJump;
	}

	private void Start() =>
		StartCoroutine(FallRoutine());

	private void FixedUpdate() =>
		Move();

	private void Fall()
	{
		_isGrounded = false;
		StartCoroutine(FallRoutine());
	}

	private void OnCancelHorizontalMove() =>
		_movementDirection = new Vector2(0, _movementDirection.y);

	private void OnHorizontalMove(float direction) =>
		_movementDirection = new Vector2(direction, _movementDirection.y);

	private void Move()
	{
		Vector2 normalizedDirection = _surfaceInformant.GetProjection(_movementDirection);

		_offset = _inertiaDirection + (normalizedDirection * _moveSpeed);

		CheckGround();
		
		if (_offset.y <= _maxFallSpeed)
			_offset.y = _maxFallSpeed;

		_rigidbody2D.position += _offset * Time.deltaTime;
	}

	private void OnJump()
	{
		if (_isGrounded == false)
			return;

		var velocity = _rigidbody2D.velocity;

		Vector2 jumpDirection = new Vector2(velocity.x, _jumpForce);
		velocity = jumpDirection;
		_inertiaDirection = velocity;
	}

	private void OnStopJump()
	{
		const float ZeroVerticalVelocity = 0;
		_inertiaDirection = new Vector2(_rigidbody2D.velocity.x, ZeroVerticalVelocity);
	}

	private IEnumerator FallRoutine()
	{
		while (_isGrounded == false)
		{
			Debug.Log("fall");
			_inertiaDirection.y -= _gravityModifier * _rigidbody2D.gravityScale * -Physics2D.gravity.y * Time.deltaTime;
			_inertiaDirection.x = _movementDirection.x;
			yield return null;
		}

		_inertiaDirection = Vector2.zero;
	}

	private void CheckGround()
	{
		float groundCheckAngle = 0;

		_isGrounded = Physics2D.OverlapBox(_rigidbody2D.position + _groundCheckPosition,
			_groundCheckSize, groundCheckAngle, _groundLayer);

		if (_isGrounded == false) Fall();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + _offset);
		Gizmos.DrawCube(transform.position + (Vector3)_groundCheckPosition, _groundCheckSize);
	}
}