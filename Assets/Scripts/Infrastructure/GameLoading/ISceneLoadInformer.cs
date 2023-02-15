using System;

namespace Infrastructure.GameLoading.Factory
{
	public interface ISceneLoadInformer : IService
	{
		event Action SceneLoaded;
		void OnSceneLoaded();
	}
}