using System;
using Infrastructure.Data.PersistentProgress;

namespace Infrastructure.Data
{
	[Serializable]
	public class GameProgress
	{
		public readonly PlayerProgressData PlayerProgressData;

		public GameProgress(PlayerProgressData playerProgressData)
		{
			PlayerProgressData = playerProgressData;
		}
	}
}