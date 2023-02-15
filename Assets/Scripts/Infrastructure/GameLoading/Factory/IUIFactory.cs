using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.GameLoading.Factory
{
	public interface IUIFactory : IService
	{
		Task<GameObject> CreateUI();
		event Action UICreated;
	}
}