using System;
using System.Collections.Generic;
using UI_Scripts.Shop;

namespace Infrastructure.Data
{
    [Serializable]
    public class ItemsData
    {
        private List<ItemScriptableObject> _boughtItems;

        public ItemsData(List<ItemScriptableObject> boughtItems) => 
            _boughtItems = boughtItems;

        public List<ItemScriptableObject> GetBoughtItems() =>
            _boughtItems;
        
        public void UpdateWeaponData(ItemScriptableObject itemScriptableObject) => 
            _boughtItems.Add(itemScriptableObject);
    }
}