using System;

namespace PlayerScripts.TestStateMachine
{
	public interface ITestTransition
	{
		event Action<ITestState> StateChanged;
		void Enable();
		void Disable();
	}
}