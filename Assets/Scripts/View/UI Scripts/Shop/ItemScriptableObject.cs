using System;
using Game.PlayerScripts.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace View.UI_Scripts.Shop
{
    [CreateAssetMenu(fileName = "Item", menuName = "ShopItemInfo", order = 0)]
    [Serializable]
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private Item _prefab;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _isBought;
        [SerializeField] private AssetReference _assetReference;

        public Item Prefab => _prefab;
        public string Title => _title;
        public string AssetName => _assetReference.AssetGUID;
        public string Description => _description;
        public int Price => _price;
        public Sprite Sprite => _sprite;
        public bool IsBought => _isBought;

        public void SetBought(bool isBought) => 
            _isBought = isBought;
    }
}