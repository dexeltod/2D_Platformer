using Cysharp.Threading.Tasks;
using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.Services.AssetManagement;

namespace Infrastructure.Services.Interfaces
{
	public class SceneConfigGetter : ISceneConfigGetter
	{
		private readonly IAssetProvider _assetProvider;

		public SceneConfigGetter()
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();
		}

		public async UniTask<SceneConfig> GetSceneConfig(string sceneConfigName) => 
			await _assetProvider.LoadAsync<SceneConfig>(sceneConfigName);
	}
}