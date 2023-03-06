using System.Reflection;
using Game.Enemy.Services;
using UnityEngine;

namespace Editor
{
	public static class PrivateFieldGetter
	{
		public static Transform GetTransform(EnemyPlayerChecker enemyPlayerChecker,
			string playerTransformString)
		{
			return (Transform)enemyPlayerChecker.GetType()
				?.GetField(playerTransformString, BindingFlags.Instance | BindingFlags.NonPublic)
				?.GetValue(enemyPlayerChecker);
		}

		public static bool GetBool(EnemyPlayerChecker enemyPlayerChecker, string boolName) =>
			(bool)enemyPlayerChecker.GetType()
				?.GetField(boolName, BindingFlags.Instance | BindingFlags.NonPublic)
				?.GetValue(enemyPlayerChecker);

		public static float GetFloat(object type, string floatName) =>
			(float)type.GetType()
				?.GetField(floatName, BindingFlags.Instance | BindingFlags.NonPublic)
				?.GetValue(type);
	}
}