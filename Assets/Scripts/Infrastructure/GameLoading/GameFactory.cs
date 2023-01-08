using System;
using Infrastructure.AssetProvide;
using UnityEngine;

namespace Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assetProvider;

		public GameFactory(IAssetProvider assetProvider) => 
			_assetProvider = assetProvider;

		public GameObject MainCharacter { get; private set; }
		public Transform EyeCharacterTransform { get; private set;}

		public event Action MainCharacterCreated;

		public GameObject CreateHero(GameObject initialPoint)
		{
			MainCharacter = _assetProvider.Instantiate(NameConstants.PlayerPrefabPath, initialPoint.transform.position);
			EyeCharacterTransform = MainCharacter.GetComponentInChildren<PlayerEyePoint>().transform;
			MainCharacterCreated?.Invoke();
			return MainCharacter;
		}
	}
}