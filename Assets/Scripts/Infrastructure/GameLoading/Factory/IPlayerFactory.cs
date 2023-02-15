using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.GameLoading.Factory
{
	public interface IPlayerFactory : IService
	{
		GameObject MainCharacter { get; }
		event Action MainCharacterCreated;
		Task<GameObject> CreateHero(GameObject initialPoint);
	}
}