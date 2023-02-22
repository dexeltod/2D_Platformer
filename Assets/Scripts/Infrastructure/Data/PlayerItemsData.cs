using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
using UI_Scripts.Shop;
using UnityEngine;

namespace Infrastructure.Data
{
	[Serializable]
	public class PlayerItemsData
	{
		private const string FistAddress = "FistInfo";

		private List<ItemScriptableObject> _boughtItems = new();
		private IAssetProvider _assetProvider;

		public PlayerItemsData()
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();
		}

		public async Task<ItemScriptableObject> SetDefaultWeapon()
		{
			ItemScriptableObject item = await _assetProvider.LoadAsync<ItemScriptableObject>(FistAddress);

			_boughtItems.Add(item);

			return item;
		}
		
		public List<ItemScriptableObject> GetBoughtItems() =>
			_boughtItems.ToList();

		public void AddWeaponInData(ItemScriptableObject item) =>
			_boughtItems.Add(item);
	}
}