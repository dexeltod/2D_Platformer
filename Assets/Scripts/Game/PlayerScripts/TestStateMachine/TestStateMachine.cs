namespace PlayerScripts.TestStateMachine
{
	public class TestStateMachine
	{
		private ITestState _currentState;

		public TestStateMachine(ITestState state) => 
			ChangeState(state);

		private void ChangeState(ITestState state)
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