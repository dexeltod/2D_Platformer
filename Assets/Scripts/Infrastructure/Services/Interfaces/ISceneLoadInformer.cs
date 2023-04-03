using System;

namespace Infrastructure.Services.Interfaces
{
	public interface ISceneLoadInformer : IService
	{
		event Action SceneLoaded;
	}
}