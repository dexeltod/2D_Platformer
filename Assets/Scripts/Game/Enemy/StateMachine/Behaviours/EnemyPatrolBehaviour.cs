using Game.Enemy.EnemySettings.TestEnemy.Data.ScriptableObjects;
using Game.Enemy.Services;
using Game.PlayerScripts.Move;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy.StateMachine.Behaviours
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(EnemyObserver))]
	[RequireComponent(typeof(SurfaceInformant))]
	public class EnemyPatrolBehaviour : MonoBehaviour
	{
		[SerializeField] private EnemyData _verticalSpeed;

		private SurfaceInformant _surfaceInformant;
		private Rigidbody2D _rigidbody;
		private EnemyObserver _observer;

		private bool _canMove;

		public event UnityAction NoWay;

		private void Awake()
		{
			_surfaceInformant = GetComponent<SurfaceInformant>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_observer = GetComponent<EnemyObserver>();
		}

		private void FixedUpdate()
		{
			CheckWay();
			CheckAround();
			Move();
		}

		private void OnDisable() =>
			NullifyHorizontalVelocity();

		private void NullifyHorizontalVelocity()
		{
			float minVelocity = 0;
			float minRigidbodyVelocity = _rigidbody.velocity.x * minVelocity;

			_rigidbody.position += new Vector2(minRigidbodyVelocity, _rigidbody.velocity.y);
		}

		private void CheckAround() =>
			_canMove = _observer.IsNearLedge() || !_observer.IsTouchWall();

		private void Move()
		{
			float verticalVelocity = _verticalSpeed.WalkSpeed * _observer.FacingDirection;

			Vector2 direction = new Vector2(verticalVelocity, _rigidbody.velocity.y);
			Vector2 directionAlongSlope = _surfaceInformant.GetProjectionAlongNormal(direction);

			if (_canMove)
				_rigidbody.position += directionAlongSlope * (verticalVelocity * Time.deltaTime);
		}

		private void CheckWay()
		{
			bool isNoWay = _observer.IsNearLedge() == false || _observer.IsTouchWall() == true;

			if (isNoWay)
			{
				NoWay.Invoke();
				_observer.RotateFacingDirection();
			}
		}
	}
}