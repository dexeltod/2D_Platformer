using System;

namespace Game.PlayerScripts.StateMachine
{
	public interface IStateTransition
	{
		event Action<IState> StateChanged;
		void OnEnable();
		void OnDisable();
	}
}