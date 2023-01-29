using System;

namespace Game.PlayerScripts.TestStateMachine
{
	public interface IState
	{
		public event Action<IState> StateChanged;

		void Enter();
		void Exit();
	}
}