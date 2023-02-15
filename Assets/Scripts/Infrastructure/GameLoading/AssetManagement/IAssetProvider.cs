using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.GameLoading.AssetManagement
{
	public interface IAssetProvider : IService
	{
		Task<GameObject> Instantiate(string path);
		Task<GameObject> Instantiate(string path, Vector3 position);
		Task<T> LoadAsync<T>(string address) where T : class;
		void CleanUp();
	}
}