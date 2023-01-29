namespace Game.PlayerScripts.TestStateMachine
{
	public class TestStateMachine
	{
		private IState _currentState;

		public TestStateMachine(IState state) => 
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