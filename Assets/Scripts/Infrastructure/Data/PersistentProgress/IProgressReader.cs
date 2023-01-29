namespace Infrastructure.Data.PersistentProgress
{
    public interface IProgressReader
    {
        void Read(PlayerProgress progress);
    }
}