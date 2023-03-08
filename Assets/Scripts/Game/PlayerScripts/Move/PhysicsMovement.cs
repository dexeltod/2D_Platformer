using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerScripts.Move
{
	[RequireComponent(typeof(SurfaceInformant))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class PhysicsMovement : MonoBehaviour
	{
		private const float MinFallVelocity = -0.5f;
		[SerializeField] private Transform _feetPosition;
		[SerializeField] private WallCheckTrigger _wallCheckTrigger;

		[SerializeField] private bool _isDebug;
		[SerializeField] private float _jumpForce = 3f;
		[SerializeField] private float _verticalVelocityLimit = -5f;
		[SerializeField] private float _racingSpeed;
		[SerializeField] private float _maxMoveSpeed;

		[SerializeField] private float _wallJumpForce = 4.5f;
		[SerializeField] private float _maxWallSlideSpeed;
		[SerializeField] private float _wallSlideVelocity;
		[SerializeField] private float _onWallStayTime;

		private GroundChecker _groundChecker;
		private SurfaceInformant _surfaceInformant;
		private Rigidbody2D _rigidbody2D;
		private Coroutine _currentRunCoroutine;
		private IInputService _inputService;

		private Vector2 _movementDirection;
		private Vector2 _inertiaDirection;
		private Vector2 _finalOffset;

		private bool _isRotate = true;
		private bool _lastIsRotate;
		private bool _isCanMove;
		private bool _isGlide;
		private bool _isRunning;
		private bool _isRunningLast;
		private bool _isFall;
		private bool _lastIsFall;

		private float _moveSpeed;
		private int _currentJumpStopCount;
		private int _maxJumpStopCount;
		private float _defaultGravityScale;

		private Coroutine _currentWallSlideRoutine;
		

		public bool IsTouchWall { get; private set; }
		public Transform FeetPosition => _feetPosition;
		public bool IsGrounded { get; private set; }
		public Vector2 MovementDirection => _movementDirection;

		public event UnityAction Glided;
		public event UnityAction<bool> Fallen;
		public event UnityAction<bool> Running;
		public event UnityAction<bool> Rotating;

		private void Awake()
		{
			_groundChecker = GetComponent<GroundChecker>();
			_surfaceInformant = GetComponent<SurfaceInformant>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_defaultGravityScale = _rigidbody2D.gravityScale;
			_lastIsFall = !_isFall;
		}

		private void OnEnable()
		{
			_wallCheckTrigger.WallTouched += OnWallTouched;
			_groundChecker.GroundedStateSwitched += OnSwitchGroundState;
			_surfaceInformant.GlideStateSwitched += OnGlideStateSwitched;
			_surfaceInformant.Moves += OnCanMoveChange;
		}

		private void OnDisable() =>
			Unsubscribe();

		private void OnDestroy() =>
			Unsubscribe();

		private void Unsubscribe()
		{
			_wallCheckTrigger.WallTouched -= OnWallTouched;
			_groundChecker.GroundedStateSwitched -= OnSwitchGroundState;
			_surfaceInformant.GlideStateSwitched -= OnGlideStateSwitched;
			_surfaceInformant.Moves -= OnCanMoveChange;
		}

		private void FixedUpdate() =>
			Move();

		public void StartWallSliding() =>
			StartWallSlideRoutine();

		public void SetMoveDirection(float direction)
		{
			const int ZeroVerticalDirection = 0;

			if (_currentRunCoroutine != null)
			{
				StopCoroutine(_currentRunCoroutine);
				_currentRunCoroutine = null;
			}

			if (direction != 0)
				_currentRunCoroutine = StartCoroutine(StartRunRoutine());

			_movementDirection = new Vector2(direction, ZeroVerticalDirection);
		}

		public void DoWallJump()
		{
			if (IsGrounded == false && IsTouchWall == false)
				return;

			float rotateDirection = _isRotate ? 0.5f : -0.5f;

			_isRotate = !_isRotate;
			Vector2 direction = new Vector2(rotateDirection, 0.9f);
			
			_finalOffset += direction;
			_maxJumpStopCount++;
			
		}
		
		public void Jump()
		{
			if (IsGrounded == false && IsTouchWall == false)
				return;

			_rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
			_maxJumpStopCount++;
		}

		private void OnWallTouched(bool isWallTouched)
		{
			IsTouchWall = isWallTouched;
		}

		private void OnSwitchGroundState(bool isGrounded) =>
			IsGrounded = isGrounded;

		private void OnCanMoveChange(bool canMove) =>
			_isCanMove = canMove;

		private void OnGlideStateSwitched(bool isGlide)
		{
			Glided?.Invoke();
			_isGlide = isGlide;
		}

		private void Move()
		{
			Vector2 normalizedDirection = _surfaceInformant.GetProjectionAlongNormal(_movementDirection.normalized);

			_finalOffset = normalizedDirection * _moveSpeed;

			_finalOffset.y = Mathf.Clamp(_finalOffset.y, -_verticalVelocityLimit, int.MaxValue);

			CheckFalling();
			CheckHorizontalDirection();
			CheckRunning();
			_rigidbody2D.position += _finalOffset * Time.deltaTime;
		}

		private IEnumerator StartRunRoutine()
		{
			_moveSpeed = 0;

			while (_moveSpeed < _maxMoveSpeed)
			{
				_moveSpeed = Mathf.MoveTowards(_moveSpeed, _maxMoveSpeed, _racingSpeed);
				yield return new WaitForFixedUpdate();
			}
		}

		private void StartWallSlideRoutine()
		{
			if (_currentWallSlideRoutine != null)
			{
				StopCoroutine(_currentWallSlideRoutine);
				_currentWallSlideRoutine = null;
			}

			_currentWallSlideRoutine = StartCoroutine(WallSlideRoutine());
		}

		private IEnumerator WallSlideRoutine()
		{
			bool isWallStay = true;

			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.gravityScale = 0;
			_movementDirection.y = 0;

			float wallStayTime = Time.time + _onWallStayTime;

			WaitForFixedUpdate fixedUpdateTime = new WaitForFixedUpdate();

			while (isWallStay == true && IsTouchWall == true)
			{
				if (Time.time > wallStayTime)
					isWallStay = false;

				yield return fixedUpdateTime;
			}

			while (IsTouchWall == true)
			{
				_rigidbody2D.gravityScale =
					Mathf.MoveTowards(_rigidbody2D.gravityScale, _defaultGravityScale, _wallSlideVelocity);
				yield return fixedUpdateTime;
			}
			
			_rigidbody2D.gravityScale = _defaultGravityScale;
		}

		private void CheckFalling()
		{
			_isFall = _rigidbody2D.velocity.y < MinFallVelocity;

			if (_isFall == _lastIsFall)
				return;

			_lastIsFall = _isFall;

			if (IsTouchWall == false)
				Fallen?.Invoke(_lastIsFall);
		}

		private void CheckRunning()
		{
			_isRunning = IsGrounded == true && _isCanMove == true && _movementDirection.x != 0;

			if (_isRunning == _isRunningLast)
				return;

			_isRunningLast = _isRunning;

			Running?.Invoke(_isRunning);
		}

		private void CheckHorizontalDirection()
		{
			if (_isGlide == true || _isCanMove == false)
				return;

			if (_finalOffset.x > 0.01f)
				_isRotate = false;
			else if (_finalOffset.x < -0.01f)
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
				Gizmos.DrawLine(transform.position, (Vector2)transform.position + _finalOffset);
			}
		}

		
	}
}