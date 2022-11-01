using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts.Shop
{
    [CreateAssetMenu(fileName = "Item", menuName = "ShopItemInfo", order = 0)]
    public class ItemInfo : ScriptableObject
    {
        public Sprite Sprite;
        public int Price;
        public string Description;
    }
}