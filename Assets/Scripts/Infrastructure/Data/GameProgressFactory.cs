using Cysharp.Threading.Tasks;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;

namespace Infrastructure.Data
{
	public class GameProgressFactory
	{
		public async UniTask<GameProgress> CreateProgress()
		{
			PlayerProgressDataFactory playerProgressDataFactory = new();
			await playerProgressDataFactory.GetItems();
			PlayerProgressData playerProgressData = playerProgressDataFactory.GetPlayerProgressData();
			return new GameProgress(playerProgressData);
		}
	}
}