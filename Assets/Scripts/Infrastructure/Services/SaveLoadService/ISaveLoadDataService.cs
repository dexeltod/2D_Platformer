using Cysharp.Threading.Tasks;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;

namespace Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadDataService : IService
    {
        void SaveProgress();
        UniTask<GameProgress> LoadProgress();
        void SetStartProgress();
        void SaveToJson(string fileName, object data);
        string LoadFromJson(string fileName);
    }
}