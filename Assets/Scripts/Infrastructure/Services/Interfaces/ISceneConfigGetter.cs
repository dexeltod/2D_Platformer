using Cysharp.Threading.Tasks;
using Game.SceneConfigs;

namespace Infrastructure.Services.Interfaces
{
	public interface ISceneConfigGetter : IService
	{
		UniTask<SceneConfig> GetSceneConfig(string sceneConfigName);
	}
}