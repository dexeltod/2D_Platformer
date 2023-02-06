using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class GameProgress
    {
        public readonly PlayerItemsData PlayerItemsData;

        public GameProgress(string initialLevel)
        {
	        if (PlayerItemsData == null) 
		        PlayerItemsData = new PlayerItemsData();
        }
    }
}