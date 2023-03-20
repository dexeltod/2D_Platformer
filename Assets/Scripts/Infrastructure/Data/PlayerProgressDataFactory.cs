using Cysharp.Threading.Tasks;
using Infrastructure.Data.PersistentProgress;

namespace Infrastructure.Data
{
	public class PlayerProgressDataFactory
	{
		private readonly PlayerItemsDataFactory _itemsDataFactory;

		private SerializableItemsData _itemsData;

		public PlayerProgressDataFactory()
		{
			_itemsDataFactory = new PlayerItemsDataFactory();

		}

		public PlayerProgressData GetPlayerProgressData()
		{
			return new PlayerProgressData(_itemsData);
		}

		public async UniTask GetItems()
		{
			 await _itemsDataFactory.InitializeDefaultWeaponAsync();
			_itemsData = _itemsDataFactory.GetSerializablePlayerItemsData();
		}

		private void SetItems()
		{
		}
	}
}