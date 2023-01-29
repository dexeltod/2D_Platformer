using System;
using System.Collections.Generic;
using UI_Scripts.Shop;

namespace Infrastructure.Data
{
    [Serializable]
    public class ItemsData
    {
        private List<Item> _boughtItems;

        public ItemsData(List<Item> boughtItems) => 
            _boughtItems = boughtItems;

        public List<Item> GetBoughtItems() =>
            _boughtItems;
        
        public void UpdateWeaponData(Item item) => 
            _boughtItems.Add(item);
    }
}