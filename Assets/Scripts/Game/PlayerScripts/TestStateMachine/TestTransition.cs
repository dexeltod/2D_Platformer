using System;

namespace PlayerScripts.TestStateMachine
{
	public class TestTransition<T> : ITestTransition where T : class, ITestState
	{
		private readonly StateService _stateService;

		public TestTransition(StateService stateService)
		{
			_stateService = stateService;
		}

		protected void MoveNextState() =>
			StateChanged?.Invoke(_stateService.Get<T>());

		public event Action<ITestState> StateChanged;

		public virtual void Enable()
		{
		}

		public virtual void Disable()
		{
		}
	}
}