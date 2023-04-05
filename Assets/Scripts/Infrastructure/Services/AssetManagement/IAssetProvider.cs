using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
	public interface IAssetProvider : IService
	{
		UniTask<GameObject> Instantiate(string path);
		UniTask<GameObject> Instantiate(string path, Vector3 position);
		UniTask<GameObject> InstantiateNoCash(string path, Vector3 position);
		UniTask<T> LoadAsync<T>(string address) where T : class;
		UniTask<T> LoadAsyncByGUID<T>(string address) where T : class;
		UniTask<T> LoadAsyncWithoutCash<T>(string address) where T : class;
		void CleanUp();
	}
}