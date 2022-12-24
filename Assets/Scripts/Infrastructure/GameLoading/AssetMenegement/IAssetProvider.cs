using UnityEngine;

namespace Infrastructure.AssetProvide
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector3 position);
	}
}