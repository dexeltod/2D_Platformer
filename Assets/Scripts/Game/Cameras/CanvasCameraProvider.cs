using Infrastructure.GameLoading;
using Infrastructure.Services;
using UnityEngine;

namespace Game.Cameras
{
	[RequireComponent(typeof(Canvas))]
	public class CanvasCameraProvider : MonoBehaviour
	{
		private Canvas _canvas;
		private ISceneLoadInformer _sceneLoadInformer;

		private void Awake()
		{
			ISceneLoadInformer sceneLoadInformer = ServiceLocator.Container.GetSingle<ISceneLoadInformer>();
			_sceneLoadInformer = sceneLoadInformer;
		
			_sceneLoadInformer.SceneLoaded += OnSceneLoaded;
		}

		private void OnSceneLoaded()
		{
			_canvas = GetComponent<Canvas>();
			_canvas.worldCamera = ServiceLocator.Container.GetSingle<ICamera>().Camera;
			_sceneLoadInformer.SceneLoaded -= OnSceneLoaded;
		}
	}
}