using System;

namespace Infrastructure.Data
{
    [Serializable]
	public class PlayerProgress
    {
        private WeaponData _weaponData;

        public PlayerProgress(string initialLevel)
        {
            
        }

        public void UpdateWeaponData()
        {
            _weaponData.UpdateWeaponData();
        }
    }
}