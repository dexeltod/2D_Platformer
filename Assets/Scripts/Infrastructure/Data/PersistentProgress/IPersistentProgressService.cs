using Infrastructure.GameLoading;

namespace Infrastructure.Data.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		GameProgress GameProgress { get; set; }
	}
}