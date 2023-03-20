using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
using UI_Scripts.Shop;

namespace Infrastructure.Data
{
	
	public class PlayerItemsData
	{
		private readonly string[] _items;

		private readonly List<ItemScriptableObject> _boughtItems;
		private IAssetProvider _assetProvider;

		public PlayerItemsData(List<ItemScriptableObject> startItems)
		{
			
			_boughtItems = startItems;
		}

		public List<ItemScriptableObject> GetBoughtItems() =>
			_boughtItems.ToList();

		public void AddWeaponInData(ItemScriptableObject item) =>
			_boughtItems.Add(item);
	}
}