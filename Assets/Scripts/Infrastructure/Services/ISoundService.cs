using Infrastructure.GameLoading;

namespace Infrastructure.Services
{
	public interface ISoundService : IService
	{
		void Set(string audioName);
		void Stop();
	}
}