using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
	public interface ICameraFactory : ICamera
	{
		Task<GameObject> CreateCamera();
	}
}