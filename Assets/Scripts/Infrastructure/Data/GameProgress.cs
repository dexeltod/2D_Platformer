using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class GameProgress
    {
        public readonly ItemsData ItemsData;

        public GameProgress(string initialLevel)
        {
        }
    }
}