using Infrastructure.Data;

namespace Infrastructure.GameLoading
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}