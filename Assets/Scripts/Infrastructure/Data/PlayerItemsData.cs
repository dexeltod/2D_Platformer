using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
using UI_Scripts.Shop;

namespace Infrastructure.Data
{
	[Serializable]
	public class PlayerItemsData
	{
		private const string FistAddress = "Fist";

		private List<ItemScriptableObject> _boughtItems = new();
		private IAssetProvider _assetProvider;

		public PlayerItemsData()
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();

			AddFist();
			_assetProvider.CleanUp();
		}

		public List<ItemScriptableObject> GetBoughtItems() =>
			_boughtItems.ToList();

		public void UpdateWeaponData(ItemScriptableObject item) =>
			_boughtItems.Add(item);

		private async Task<ItemScriptableObject> AddFist()
		{
			ItemScriptableObject item = await _assetProvider.LoadAsync<ItemScriptableObject>(FistAddress);
			_boughtItems.Add(item);

			return item;
		}
	}
}