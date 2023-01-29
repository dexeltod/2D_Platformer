using UnityEngine;

namespace Infrastructure.GameLoading.AssetMenegement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab);
		}

		public GameObject Instantiate(string path, Vector3 position)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab, position, Quaternion.identity);
		}
	}
}