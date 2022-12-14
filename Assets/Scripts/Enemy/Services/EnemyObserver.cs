using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyObserver : MonoBehaviour
{
	[SerializeField] private DataEntityVisibility _entityVisibility;
	[SerializeField] private EnemyPlayerChecker _enemyPlayerChecker;
	[SerializeField] private EnemyMeleePlayerChecker _enemyMeleeChecker;

	[SerializeField] private Transform _wallCheckTransform;
	[SerializeField] private Transform _ledgeCheckTransform;

	[Header("Debug")] [SerializeField] private bool _isEnableGizmos;

	public event Action<bool> TouchedPlayer;
	public event Action<bool> SeenPlayer;

	public int FacingDirection { get; private set; }

	private void Start() =>
		FacingDirection = 1;

	private void OnEnable()
	{
		_enemyMeleeChecker.enabled = true;
		_enemyPlayerChecker.enabled = true;
		_enemyMeleeChecker.TouchedPlayer += OnTouchPlayer;
		_enemyPlayerChecker.SeenPlayer += OnSeeEnemy;
	}

	private void OnDisable()
	{
		_enemyMeleeChecker.TouchedPlayer -= OnTouchPlayer;
		_enemyPlayerChecker.SeenPlayer -= OnSeeEnemy;
		_enemyMeleeChecker.enabled = false;
		_enemyPlayerChecker.enabled = false;
	}

	private void OnSeeEnemy(bool isSeeEnemy) => 
		SeenPlayer.Invoke(isSeeEnemy);

	private void OnTouchPlayer(bool isSeeEnemy) => 
		TouchedPlayer?.Invoke(isSeeEnemy);

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