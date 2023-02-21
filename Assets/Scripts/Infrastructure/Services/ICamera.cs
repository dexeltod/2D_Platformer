using Infrastructure.GameLoading;
using UnityEngine;

namespace Infrastructure.Services
{
	public interface ICamera : IService
	{
		Camera Camera { get; }
	}
}