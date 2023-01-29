using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using UI_Scripts.Shop;
using UnityEngine;

namespace Game.PlayerScripts.ItemScriptableObjects.WeaponList
{
    public class WeaponList : ScriptableObject, ISavedProgress
    {
        [SerializeField] private List<Item> _weapon;

        public void GetWeapon(Item weapon)
        {
            //TODO: Need to get weapon;
        }
	
        public void AddWeapon(Item weapon)
        {
            _weapon.Add(weapon);
        }

        private void FormatToBinary()
        {
            BinaryFormatter formetter;
        }

        public void Update(GameProgress progress)
        {
            throw new NotImplementedException();
        }

        public void Load(GameProgress progress)
        {
            throw new NotImplementedException();
        }
    }
}