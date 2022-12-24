using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPlayerChecker))]
public class EnemyPlayerCheckerEditor : Editor
{
	[DrawGizmo(GizmoType.NonSelected)]
	public static void RenderCustomGizmo(EnemyPlayerChecker fov, GizmoType gizmo)
	{
		var position = fov.EyeTransform.position;
		var eulerAngles = fov.transform.eulerAngles;
		var angles = fov.EyeTransform.eulerAngles;

		Gizmos.color = Color.yellow;

		if (fov.CanSeePlayer)
		{
			Handles.color = Color.green;
			Handles.DrawLine(fov.transform.position, fov.EyeTransform.transform.position);
		}

		Vector3 angle01 = DirectionFromAngle(-angles.z, -fov.AngleView / 2);
		Vector3 angle02 = DirectionFromAngle(-angles.z, fov.AngleView / 2);

		Gizmos.DrawLine(position, position + angle01 * fov.ViewDistance);
		Gizmos.DrawLine(position, position + angle02 * fov.ViewDistance);
	}

	private static Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
	{
		angleInDegrees += eulerY;
		return new Vector2(MathF.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
