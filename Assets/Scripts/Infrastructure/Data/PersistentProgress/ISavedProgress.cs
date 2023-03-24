using Infrastructure.Data.Serializable;

namespace Infrastructure.Data.PersistentProgress
{
    public interface ISavedProgress : IProgressReader
    {
        void Load(GameProgress progress);
    }
}