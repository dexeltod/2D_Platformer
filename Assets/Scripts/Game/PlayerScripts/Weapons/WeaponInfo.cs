using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.PlayerScripts.Weapons
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon/WeaponInfo", order = 0)]
	public class WeaponInfo : ScriptableObject
	{
		public int Damage;
		public float AttackSpeed;
		public float Range;
		public AssetReference WeaponSound;
		public AssetReference DamageSound;
		public AssetReference DamageSoundPlayer;
	}
}