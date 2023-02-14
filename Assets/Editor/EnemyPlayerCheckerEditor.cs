using System;
using System.Reflection;
using Game.Enemy.Services;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPlayerChecker))]
public class EnemyPlayerCheckerEditor : Editor
{
	private const string AngleViewString = "_angleView";
	private const string CanSeePlayerString = "_сanSeePlayer";
	private const string PlayerTransformString = "_playerTransform";
	private const string EyeTransformString = "_eyeTransform";

	private static Vector3 _position;
	private static Vector3 _angles;
	private static Transform _playerTransform;
	private static Transform _eyeTransform;
	private static float _angleDirection;
	private static float _angleView;
	private static bool _canSeePlayer;

	[DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
	public static void RenderCustomGizmo(EnemyPlayerChecker enemyPlayerChecker, GizmoType gizmo)
	{
		_angleDirection = 90f;
		GetPrivateFields(enemyPlayerChecker);

		SetAnglesAngPosition();


		Vector3 lineDown = GetDirectionFromAngle(-_angles.y + _angleDirection, -_angleView / 2);
		Vector3 lineUp = GetDirectionFromAngle(-_angles.y + _angleDirection, _angleView / 2);

		DrawLines(enemyPlayerChecker, lineDown, lineUp);
		
		Gizmos.color = new Color(0.33f, 0.31f, 1f, 0.37f);

		Gizmos.DrawLine(_position, _position + lineDown * enemyPlayerChecker.ViewDistance);
		Gizmos.DrawLine(_position, _position + lineUp * enemyPlayerChecker.ViewDistance);
		Gizmos.DrawWireSphere(_position, enemyPlayerChecker.ViewDistance);
		
		CheckPlayerVisibility();
	}

	private static void SetAnglesAngPosition()
	{
		_position = _eyeTransform.position;
		_angles = _eyeTransform.eulerAngles;
	}

	private static void GetPrivateFields(EnemyPlayerChecker enemyPlayerChecker)
	{
		_angleView = GetFloatFieldByReflection(enemyPlayerChecker, AngleViewString);
		_canSeePlayer = GetBoolFieldByReflection(enemyPlayerChecker, CanSeePlayerString);
		_playerTransform = GetTransformFieldByReflection(enemyPlayerChecker, PlayerTransformString);
		_eyeTransform = GetTransformFieldByReflection(enemyPlayerChecker, EyeTransformString);
	}

	private static Transform GetTransformFieldByReflection(EnemyPlayerChecker enemyPlayerChecker, string playerTransformString)
	{
		return (Transform)enemyPlayerChecker.GetType()
			?.GetField(playerTransformString, BindingFlags.Instance | BindingFlags.NonPublic)
			?.GetValue(enemyPlayerChecker);
	}

	private static bool GetBoolFieldByReflection(EnemyPlayerChecker enemyPlayerChecker, string boolName) =>
		(bool)enemyPlayerChecker.GetType()
			?.GetField(boolName, BindingFlags.Instance | BindingFlags.NonPublic)
			?.GetValue(enemyPlayerChecker);

	private static float GetFloatFieldByReflection(object type, string floatName) =>
		(float)type.GetType()
			?.GetField(floatName, BindingFlags.Instance | BindingFlags.NonPublic)
			?.GetValue(type);

	private static void CheckPlayerVisibility()
	{
		if (_canSeePlayer)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_eyeTransform.position, _playerTransform.position);
		}
	}

	private static Vector2 GetDirectionFromAngle(float eulerY, float angleInDegrees)
	{
		angleInDegrees += eulerY;
		return new Vector2(MathF.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	private static void DrawLines(EnemyPlayerChecker enemyPlayerChecker, Vector3 firstLine, Vector3 secondLine)
	{
		
	}
}