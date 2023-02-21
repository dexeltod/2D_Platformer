using System;

namespace Infrastructure.GameLoading
{
	public interface ISceneLoadInformer : IService
	{
		event Action SceneLoaded;
	}
}