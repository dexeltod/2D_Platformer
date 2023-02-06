using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.GameLoading
{
	public interface IPlayerFactory : IService
	{
		GameObject MainCharacter { get; }
		event Action MainCharacterCreated;
		Task<GameObject> CreateHero(GameObject initialPoint);
	}
}