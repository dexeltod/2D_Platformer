using Cysharp.Threading.Tasks;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;

namespace Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        UniTask<GameProgress> LoadProgress();
        void SetStartProgress();
    }
}