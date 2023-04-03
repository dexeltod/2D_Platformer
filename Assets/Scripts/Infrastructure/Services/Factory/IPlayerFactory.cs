using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
	public interface IPlayerFactory : IService
	{
		GameObject MainCharacter { get; }
		event Action MainCharacterCreated;
		UniTask InstantiateHero(GameObject initialPoint);
	}
}