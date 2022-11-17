using UnityEngine;

public class SurfaceInformant : MonoBehaviour
{
	private Vector2 _normal;

	private void OnCollisionStay2D(Collision2D collision) =>
		_normal = collision.contacts[0].normal;

	public Vector2 GetProjection(Vector2 enterDirection)
	{
		float scalar = Vector2.Dot(enterDirection, _normal);

		Vector2 scalarNormal = scalar * _normal;

		if (enterDirection == scalarNormal)
			return scalarNormal;

		Vector2 directionAlongSurface = enterDirection - scalarNormal;
		return directionAlongSurface;
	}
}