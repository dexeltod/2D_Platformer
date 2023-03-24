using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using UI_Scripts.Shop;
using UnityEngine;

namespace Game.PlayerScripts.ItemScriptableObjects.WeaponList
{
    public class WeaponList : ScriptableObject, ISavedProgress
    {
        [SerializeField] private List<ItemScriptableObject> _weapon;

        public void GetWeapon(ItemScriptableObject weapon)
        {
            //TODO: Need to get weapon;
        }
	
        public void AddWeapon(ItemScriptableObject weapon)
        {
            _weapon.Add(weapon);
        }

        private void FormatToBinary()
        {
            BinaryFormatter formatter;
        }

        public void Reload(GameProgress progress)
        {
            //TODO: Need to update weapon;
        }

        public void Load(GameProgress progress)
        {
	        //TODO: Need to load weapon;
        }
    }
}