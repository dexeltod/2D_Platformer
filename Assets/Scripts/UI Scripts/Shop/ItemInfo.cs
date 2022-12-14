using PlayerScripts.Weapons;
using UnityEngine;

namespace UI_Scripts.Shop
{
	[CreateAssetMenu(fileName = "Item", menuName = "ShopItemInfo", order = 0)]
	public class ItemInfo : ScriptableObject
	{
		public WeaponBase WeaponBase;
		public string Title;
		public string Description;
		public int Price;
		public Sprite Sprite;
	}
}