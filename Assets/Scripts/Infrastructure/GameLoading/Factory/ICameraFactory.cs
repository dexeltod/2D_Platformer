using System.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLoading.Factory
{
	public interface ICameraFactory : ICamera
	{
		Task<GameObject> CreateCamera();
	}
}