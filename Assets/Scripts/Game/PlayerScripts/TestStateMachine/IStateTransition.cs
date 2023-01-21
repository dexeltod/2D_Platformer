using System;

namespace PlayerScripts.TestStateMachine
{
	public interface IStateTransition
	{
		event Action<IState> StateChanged;
		void Enable();
		void Disable();
	}
}