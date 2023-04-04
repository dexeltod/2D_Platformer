using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using Infrastructure.Services.AssetManagement;
using View.UI_Scripts.Shop;

namespace Infrastructure.Data
{
	public class PlayerItemsDataFactory
	{
		private const string FistAddress = "FistSO";

		private readonly IAssetProvider _assetProvider;
		private readonly List<ItemScriptableObject> _items = new();

		public PlayerItemsData ItemsData { get; private set; }

		public PlayerItemsDataFactory()
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();
			ItemsData = new(_items);
		}

		public SerializableItemsData GetSerializablePlayerItemsData()
		{
			return new SerializableItemsData(GetItemsTitles(), GetItemsReferences());
		}

		public async UniTask InitializeDefaultWeaponAsync()
		{
			var result = await _assetProvider.LoadAsync<ItemScriptableObject>(FistAddress);
			AddItem(result);
		}

		private void AddItem(ItemScriptableObject result)
		{
			_items.Add(result);
		}
		
		private string[] GetItemsReferences()
		{
			var items = ItemsData.GetBoughtItems();
			string[] itemReferences = new string[items.Count];

			for (int i = 0; i < items.Count; i++)
			{
				itemReferences[i] = items[i].AssetGUID;
			}

			return itemReferences;
		}

		private string[] GetItemsTitles()
		{
			var items = ItemsData.GetBoughtItems();

			string[] itemTitles = new string[items.Count];

			for (int i = 0; i < items.Count; i++)
				itemTitles[i] = items[i].Title;

			return itemTitles;
		}
	}
}