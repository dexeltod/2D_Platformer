namespace Infrastructure.Data.PersistentProgress
{
    public interface IProgressReader
    {
        void Update(GameProgress progress);
    }
}