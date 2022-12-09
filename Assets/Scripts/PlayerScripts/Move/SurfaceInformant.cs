using System;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceInformant : MonoBehaviour
{
	[SerializeField] private bool _isDebug;
	[SerializeField] private Transform _center;
	[SerializeField, Range(0, 180)] private float _maxSlopeAngle;

	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private float _slopeCheckDistance;

	private Vector2 _normal;
	private Vector2 _capsuleColliderSize;
	private Vector2 _slopeNormalPerp;

	private float _slopeSideAngle;
	private float _slopeDownAngle;
	private float _lastSlopeAngle;
	private float _moveDirectionX;

	private bool _isGlideLast;
	private bool _isGlide;
	private bool _isOnSlope;
	private bool _canWalkOnSlope;
	private bool _canWalkOnSlopeLast;

	public event UnityAction<bool> GlideStateSwitched;
	public event UnityAction<bool> Moves;

	public Vector2 GetProjectionAlongNormal(Vector2 enterDirection)
	{
		CheckAngleSurface();
		SlopeCheckVertical();

		RaycastHit2D hit = Physics2D.Raycast(
			transform.position,
			Vector2.down,
			_slopeCheckDistance,
			_groundLayer);

		Vector2 directionAlongSurface = enterDirection;

		if (hit)
		{
			_normal = hit.normal;
			_slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
			directionAlongSurface.Set(-enterDirection.x * _slopeNormalPerp.x, -enterDirection.x * _slopeNormalPerp.y);
		}
		
		return directionAlongSurface;
	}

	private void CheckAngleSurface()
	{
		_isGlide = _lastSlopeAngle > _maxSlopeAngle;

		if (_isGlideLast == _isGlide)
			return;

		_isGlideLast = _isGlide;
		GlideStateSwitched?.Invoke(_isGlide);
	}

	private void SlopeCheckVertical()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _slopeCheckDistance, _groundLayer);

		if (hit)
		{
			_slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

			_slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
			_lastSlopeAngle = _slopeDownAngle;
		}

		if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
			_canWalkOnSlope = false;
		else
			_canWalkOnSlope = true;

		if (_canWalkOnSlope == _canWalkOnSlopeLast)
			return;

		_canWalkOnSlopeLast = _canWalkOnSlope;
		Moves.Invoke(_canWalkOnSlope);
	}

	private void OnDrawGizmos()
	{
		if (_isDebug == true)
		{
			Gizmos.color = new Color(0.2f, 0.2f, 1f);
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)_normal);
		}
	}
}