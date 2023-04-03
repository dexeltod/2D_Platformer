namespace Infrastructure.Services.Interfaces
{
	public interface IMusicService : IService
	{
		void Set(string audioName);
		void Stop();
	}
}