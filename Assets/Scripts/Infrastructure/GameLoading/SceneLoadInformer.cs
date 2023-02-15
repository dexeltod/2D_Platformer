using System;

namespace Infrastructure.GameLoading.Factory
{
	public class SceneLoadInformer: ISceneLoadInformer
	{
		public event Action SceneLoaded;

		public void OnSceneLoaded()
		{
			SceneLoaded?.Invoke();
		}
	}
}