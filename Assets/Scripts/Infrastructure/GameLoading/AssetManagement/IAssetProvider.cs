using UnityEngine;

namespace Infrastructure.GameLoading.AssetMenegement
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector3 position);
	}
}