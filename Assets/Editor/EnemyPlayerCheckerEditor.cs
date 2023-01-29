using System;
using Game.Enemy.Services;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPlayerChecker))]
public class EnemyPlayerCheckerEditor : Editor
{
	private static float _angleDirection;

	[DrawGizmo(GizmoType.NonSelected)]
	public static void RenderCustomGizmo(EnemyPlayerChecker fov, GizmoType gizmo)
	{
		_angleDirection = 90f;

		var position = fov.EyeTransform.position;
		var angles = fov.EyeTransform.eulerAngles;

		Gizmos.color = new Color(0.33f, 0.31f, 1f, 0.37f);

		if (fov.CanSeePlayer)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(fov.EyeTransform.position, fov.PlayerTransform.position);
		}

		Vector3 angle01 = DirectionFromAngle(-angles.y + _angleDirection, -fov.AngleView / 2);
		Vector3 angle02 = DirectionFromAngle(-angles.y + _angleDirection, fov.AngleView / 2);

		Gizmos.DrawLine(position, position + angle01 * fov.ViewDistance);
		Gizmos.DrawLine(position, position + angle02 * fov.ViewDistance);
		Gizmos.DrawWireSphere(position, fov.ViewDistance);
	}

	private static Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
	{
		angleInDegrees += eulerY;
		return new Vector2(MathF.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}