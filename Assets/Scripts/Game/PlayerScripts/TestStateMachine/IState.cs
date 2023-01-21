using System;

namespace PlayerScripts.TestStateMachine
{
	public interface IState
	{
		public event Action<IState> StateChanged;

		void Enter();
		void Exit();
	}
}