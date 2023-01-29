using Infrastructure.Data;

namespace Infrastructure
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress PlayerProgress { get; set; }
	}
} 