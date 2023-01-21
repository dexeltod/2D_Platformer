using PlayerScripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI_Scripts.Shop
{
	[CreateAssetMenu(fileName = "Item", menuName = "ShopItemInfo", order = 0)]
	public class ItemInfo : ScriptableObject
	{
		[FormerlySerializedAs("WeaponBase")] public AbstractWeapon _abstractWeapon;
		public string Title;
		public string Description;
		public int Price;
		public Sprite Sprite;
	}
}