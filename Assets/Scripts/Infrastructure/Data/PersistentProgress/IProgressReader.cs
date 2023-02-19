namespace Infrastructure.Data.PersistentProgress
{
    public interface IProgressReader
    {
        void Reload(GameProgress progress);
    }
}