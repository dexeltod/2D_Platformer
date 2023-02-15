using System;
using System.Threading.Tasks;
using Infrastructure.GameLoading.AssetManagement;
using UnityEngine;

namespace Infrastructure.GameLoading.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UserInterface = "UI";
		private readonly IAssetProvider _assetProvider;

		public event Action UICreated;

		public UIFactory()
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();
		}
		
		public async Task<GameObject> CreateUI()
		{
			var instance = await _assetProvider.Instantiate(UserInterface);
			UICreated?.Invoke();
			return instance;
		}
	}
}