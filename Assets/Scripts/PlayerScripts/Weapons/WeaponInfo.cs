using UnityEngine;

namespace PlayerScripts.Weapons
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon/WeaponInfo", order = 0)]
	public class WeaponInfo : ScriptableObject
	{
		public int Damage;
		public float AttackSpeed;
		public float Range;
	}
}