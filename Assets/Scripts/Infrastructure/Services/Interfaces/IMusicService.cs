using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.Interfaces
{
	public interface IMusicService : IService
	{
		UniTask Set(string audioName);
		void Stop();
	}
}