using System;

namespace PlayerScripts.TestStateMachine
{
	public interface ITestState
	{
		public event Action<ITestState> StateChanged;

		void Enter();
		void Exit();
	}
}