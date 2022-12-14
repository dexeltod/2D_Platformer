using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyObserver))]
public class EnemyPatrolBehaviour : MonoBehaviour
{
	[SerializeField] private EnemyData _verticalSpeed;

	private Rigidbody2D _rigidbody;
	private EnemyObserver _observer;

	private bool _canMove;

	public event UnityAction NoWay;

	private void NullifyHorizontalVelocity()
	{
		float minVelocity = 0;
		float minRigidbodyVelocity = _rigidbody.velocity.x * minVelocity;

		_rigidbody.velocity = new Vector2(minRigidbodyVelocity, _rigidbody.velocity.y);
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_observer = GetComponent<EnemyObserver>();
	}

	private void FixedUpdate()
	{
		ChangeStateToIdle();
		CheckAround();
		Patrol();
	}

	private void OnDisable()
	{
		NullifyHorizontalVelocity();
	}

	private void CheckAround() =>
		_canMove = _observer.IsNearLedge() || !_observer.IsTouchWall();

	private void Patrol()
	{
		float verticalVelocity = _verticalSpeed.WalkSpeed * _observer.FacingDirection;

		if (_canMove)
			_rigidbody.velocity = new Vector2(verticalVelocity, _rigidbody.velocity.y);
	}

	private void ChangeStateToIdle()
	{
		bool isNoWay = _observer.IsNearLedge() == false || _observer.IsTouchWall() == true;

		if (isNoWay)
		{
			NoWay.Invoke();
			_observer.RotateFacingDirection();
		}
	}
}