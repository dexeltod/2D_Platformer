using System;
using UnityEngine;

namespace Infrastructure
{
	public interface IGameFactory : IService
	{
		GameObject MainCharacter { get; }
		Transform EyeCharacterTransform { get; }
		event Action MainCharacterCreated;
		GameObject CreateHero(GameObject initialPoint);
	}
}