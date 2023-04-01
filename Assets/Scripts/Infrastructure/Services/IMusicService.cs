using Infrastructure.GameLoading;

namespace Infrastructure.Services
{
	public interface IMusicService : IService
	{
		void Set(string audioName);
		void Stop();
	}
}