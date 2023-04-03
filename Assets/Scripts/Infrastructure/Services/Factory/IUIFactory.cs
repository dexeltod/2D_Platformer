using System;
using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
	public interface IUIFactory : IService
	{
		Task<GameObject> CreateUI();
		event Action UICreated;
	}
}