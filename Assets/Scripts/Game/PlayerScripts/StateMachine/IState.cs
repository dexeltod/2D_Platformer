using System;

namespace Game.PlayerScripts.StateMachine
{
	public interface IState
	{
		public event Action<IState> StateChanged;

		void Enter();
		void Exit();
	}
}