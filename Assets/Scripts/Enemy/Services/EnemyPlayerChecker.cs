using System;
using UnityEngine;

public class EnemyPlayerChecker : MonoBehaviour
{
	[SerializeField] private Transform _eyePosition;
	[SerializeField] private ContactFilter2D _playerLayer;
	[SerializeField] private Vector2[] _bigColliderPoints;
	[SerializeField] private Vector2[] _smallColliderPoints;

	private PolygonCollider2D _polygonCollider;
	public event Action<bool> SeenPlayer;

	private void Awake() =>
		_polygonCollider = GetComponent<PolygonCollider2D>();

	private void Start() =>
		_smallColliderPoints = _polygonCollider.points;

	private void OnEnable()
	{
		_polygonCollider.enabled = true;
	}

	private void OnDisable()
	{
		_polygonCollider.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D targetCollider)
	{
		if (targetCollider.TryGetComponent(out Player target))
		{
			Vector2 playerPosition = target.transform.position;

			bool isHitPlayer = Physics2D.Raycast(_eyePosition.position, playerPosition, _playerLayer.layerMask);

			if (isHitPlayer)
			{
				_polygonCollider.points = _bigColliderPoints;
				SeenPlayer.Invoke(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D targetCollider)
	{
		if (targetCollider.TryGetComponent(out Player _))
		{
			_polygonCollider.points = _smallColliderPoints;
			SeenPlayer?.Invoke(false);
		}
	}
}