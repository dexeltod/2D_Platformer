using UnityEngine;

namespace Infrastructure.GameLoading.AssetManagement
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector3 position);
	}
}