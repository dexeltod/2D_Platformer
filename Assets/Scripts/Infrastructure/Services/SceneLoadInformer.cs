using System;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services
{
	public class SceneLoadInformer : ISceneLoad
	{
		public event Action SceneLoaded;

		public void InvokeSceneLoaded() =>
			SceneLoaded?.Invoke();
	}
}