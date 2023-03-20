using Cysharp.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.GameLoading;

namespace Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        UniTask<GameProgress> LoadProgress();
    }
}