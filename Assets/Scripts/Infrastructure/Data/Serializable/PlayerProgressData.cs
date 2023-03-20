using System;

namespace Infrastructure.Data.PersistentProgress
{
	[Serializable]
	public class PlayerProgressData
	{
		public readonly SerializableItemsData SerializableItemsData;

		public PlayerProgressData(SerializableItemsData playerItemsData)
		{
			SerializableItemsData = playerItemsData;
		}
	}
}