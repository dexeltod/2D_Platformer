using System;
using Game.Enemy.EnemySettings.ScriptableObjectsScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Enemy.Services
{
	public class EnemyObserver : MonoBehaviour
	{
		private const int RightRotation = 0;
		private const int LeftRotation = 180;

		[FormerlySerializedAs("_entityVisibility")] [SerializeField]
		private DataEnemyVisibility _enemyVisibility;

		[SerializeField] private EnemyPlayerChecker _enemyPlayerChecker;
		[SerializeField] private EnemyMeleeTrigger _enemyMeleeChecker;
		[SerializeField] private Transform _ledgeCheckTransform;

		[Header("Debug")] [SerializeField] private bool _isEnableGizmos;

		private Collider2D _collider2D;
		public event Action<bool> TouchedPlayer;
		public event Action<bool> SeenPlayer;

		public int FacingDirection { get; private set; }

		private void Start()
		{
			_collider2D = GetComponent<Collider2D>();
			FacingDirection = transform.rotation.x == 0 ? 1 : -1;
		}

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

		public void SetFacingDirection(float normalizedDirection)
		{
			if (normalizedDirection != FacingDirection)
				RotateFacingDirection();

			int rotation = normalizedDirection > 0 ? RightRotation : LeftRotation;
			transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, 0);
		}

		private void OnTouchPlayer(bool isSeeEnemy) =>
			TouchedPlayer?.Invoke(isSeeEnemy);

		public bool IsTouchWall() =>
			Physics2D.Raycast(_collider2D.bounds.center, Vector2.right * FacingDirection,
				_enemyVisibility.WallCheckDistance, _enemyVisibility.GroundLayer);

		public bool IsNearLedge() =>
			Physics2D.Raycast(_ledgeCheckTransform.position,
				Vector2.down,
				_enemyVisibility.LedgeCheckDistance, _enemyVisibility.GroundLayer);

		public void RotateFacingDirection()
		{
			FacingDirection *= -1;
			float rotation = FacingDirection == 1 ? RightRotation : LeftRotation;

			transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);
		}

		private void OnSeeEnemy(bool isSeeEnemy) =>
			SeenPlayer.Invoke(isSeeEnemy);

		private void OnDrawGizmos()
		{
			if (_isEnableGizmos == false)
				return;

			if (_collider2D != null)
			{
				Vector3 wallCheckDirection = (Vector2)_collider2D.bounds.center +
				                             _enemyVisibility.WallCheckDistance * (Vector2.right * FacingDirection);
				Gizmos.DrawLine(_collider2D.bounds.center, wallCheckDirection);
			}

			Vector2 ledgeCheckDirection = (Vector2)_ledgeCheckTransform.position +
			                              Vector2.down * _enemyVisibility.LedgeCheckDistance;

			Gizmos.DrawLine(_ledgeCheckTransform.position, ledgeCheckDirection);
		}
	}
}