using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyObserver : MonoBehaviour
{
	[SerializeField] private DataEntityVisibility _entityVisibility;
	[SerializeField] private EnemyPlayerChecker _enemyPlayerChecker;

	[SerializeField] private Transform _wallCheckTransform;
	[SerializeField] private Transform _ledgeCheckTransform;

	[Header("Debug")] [SerializeField] private bool _isEnableGizmos;
	public event Action<bool> SeenEnemy;

	public int FacingDirection { get; private set; }

	private void Start() =>
		FacingDirection = 1;

	private void OnEnable() =>
		_enemyPlayerChecker.SeenPlayer += OnSeeEnemy;

	private void OnDisable() =>
		_enemyPlayerChecker.SeenPlayer -= OnSeeEnemy;

	public bool IsTouchWall()
	{
		return Physics2D.Raycast(_wallCheckTransform.position, Vector2.right * FacingDirection,
			_entityVisibility.WallCheckDistance, _entityVisibility.WhatIsTouched);
	}

	public bool IsNearLedge()
	{
		return Physics2D.Raycast(_ledgeCheckTransform.position,
			Vector2.down,
			_entityVisibility.LedgeCheckDistance, _entityVisibility.WhatIsGround);
	}

	private void OnSeeEnemy(bool isSeeEnemy)
	{
		if (isSeeEnemy == true)
			SeenEnemy.Invoke(isSeeEnemy);
	}

	public void RotateFacingDirection()
	{
		const int Right = 1;
		const int Left = -180;

		FacingDirection *= -1;
		int rotation = FacingDirection == 1 ? Right : Left;

		transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, 0);
	}

	private void OnDrawGizmos()
	{
		if (_isEnableGizmos != true)
			return;

		Vector3 wallCheckDirection = (Vector2)_wallCheckTransform.position
		                             + _entityVisibility.WallCheckDistance
		                             * FacingDirection * Vector2.right;

		Vector2 ledgeCheckDirection = (Vector2)_ledgeCheckTransform.position +
		                              Vector2.down * _entityVisibility.LedgeCheckDistance;

		Gizmos.DrawLine(_wallCheckTransform.position, wallCheckDirection);
		Gizmos.DrawLine(_ledgeCheckTransform.position, ledgeCheckDirection);
	}
}