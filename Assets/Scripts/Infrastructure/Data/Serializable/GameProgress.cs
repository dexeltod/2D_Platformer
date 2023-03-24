using System;

namespace Infrastructure.Data.Serializable
{
	[Serializable]
	public class GameProgress
	{
		public readonly PlayerProgressData PlayerProgressData;

		public int UnlockedLevelCount { get; private set; }
		public GameProgress(PlayerProgressData playerProgressData)
		{
			PlayerProgressData = playerProgressData;
		}

		public void SetUnlockedLevelCount(int count) => 
			UnlockedLevelCount = count;
	}
}