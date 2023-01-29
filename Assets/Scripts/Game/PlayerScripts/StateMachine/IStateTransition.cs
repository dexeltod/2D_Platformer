using System;

namespace Game.PlayerScripts.TestStateMachine
{
	public interface IStateTransition
	{
		event Action<IState> StateChanged;
		void Enable();
		void Disable();
	}
}