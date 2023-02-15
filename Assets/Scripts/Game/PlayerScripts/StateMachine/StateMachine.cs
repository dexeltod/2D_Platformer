namespace Game.PlayerScripts.StateMachine
{
	public class StateMachine
	{
		private IState _currentState;

		public StateMachine(IState state) => 
			ChangeState(state);

		private void ChangeState(IState state)
		{
			if (_currentState != null)
			{
				_currentState.StateChanged -= ChangeState;
				_currentState.Exit();
			}

			_currentState = state;
			_currentState.Enter();

			_currentState.StateChanged += ChangeState;
		}
	}
}