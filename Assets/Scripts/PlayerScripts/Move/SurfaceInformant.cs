using System;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceInformant : MonoBehaviour
{
	[SerializeField] private bool _isDebug;
	[SerializeField, Range(0, 1)] private float _minGroundNormal = 1;

	private Vector2 _normal;
	private bool _isGlideLast;
	private bool _isGlide;

	public event UnityAction<bool> Glides;

	private void OnCollisionStay2D(Collision2D collision) =>
		_normal = collision.contacts[^1].normal;

	public Vector2 GetProjection(Vector2 enterDirection)
	{
		CheckAngleSurface();
		float scalar = Vector2.Dot(enterDirection, _normal);
		ClampNormal();

		Debug.Log(_normal);
		Vector2 scalarNormal = scalar * _normal;

		if (enterDirection == scalarNormal)
			return scalarNormal;


		Vector2 directionAlongSurface = enterDirection - scalarNormal;
		return directionAlongSurface;
	}

	private void CheckAngleSurface()
	{
		_isGlide = _normal.y < _minGroundNormal;

		if (_isGlideLast == _isGlide)
			return;

		_isGlideLast = _isGlide;
		Glides?.Invoke(_isGlideLast);
	}

	private void ClampNormal()
	{
		const float UpNormalDirection = 1;
		const float MinUpNormalDirection = 0;

		_normal.y = Mathf.Clamp(_normal.y, MinUpNormalDirection, UpNormalDirection);
	}

	private void OnDrawGizmos()
	{
		if (_isDebug)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)_normal);
		}
	}
}