using System;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    [Serializable]
    public class WeaponData
    {
        private List<Item> _boughtWeapons;
        
        public void UpdateWeaponData(Item item)
        {
            _boughtWeapons.Add(item);
        }
    }
}