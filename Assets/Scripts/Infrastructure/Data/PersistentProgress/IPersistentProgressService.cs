using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Data.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		GameProgress GameProgress { get; set; }
	}
}