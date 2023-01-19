using System;
using UnityEngine;

namespace Infrastructure
{
	public interface IPlayerFactory : IService
	{
		GameObject MainCharacter { get; }
		event Action MainCharacterCreated;
		GameObject CreateHero(GameObject initialPoint);
	}
}