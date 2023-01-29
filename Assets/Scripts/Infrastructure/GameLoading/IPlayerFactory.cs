using System;
using UnityEngine;

namespace Infrastructure.GameLoading
{
	public interface IPlayerFactory : IService
	{
		GameObject MainCharacter { get; }
		event Action MainCharacterCreated;
		GameObject CreateHero(GameObject initialPoint);
	}
}