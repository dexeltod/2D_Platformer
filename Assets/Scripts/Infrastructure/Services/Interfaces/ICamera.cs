using UnityEngine;

namespace Infrastructure.Services.Interfaces
{
	public interface ICamera : IService
	{
		Camera Camera { get; }
	}
}