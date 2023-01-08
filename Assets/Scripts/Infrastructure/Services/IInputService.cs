using System;
using UnityEngine.Events;

namespace Infrastructure.Services
{
	public interface IInputService : IService
	{
		event UnityAction<float> VerticalButtonUsed;
		event UnityAction VerticalButtonCanceled;
		event UnityAction AttackButtonUsed;
		event UnityAction InteractButtonUsed;
		event UnityAction JumpButtonUsed;
		event UnityAction JumpButtonCanceled;
	}
}