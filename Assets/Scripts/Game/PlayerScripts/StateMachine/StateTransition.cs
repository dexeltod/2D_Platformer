using System;

namespace Game.PlayerScripts.TestStateMachine
{
	public class StateTransition<T> : IStateTransition where T : class, IState
	{
		private readonly StateService _stateService;

		public StateTransition(StateService stateService)
		{
			_stateService = stateService;
		}

		protected void MoveNextState() =>
			StateChanged?.Invoke(_stateService.Get<T>());

		public event Action<IState> StateChanged;

		public virtual void Enable()
		{
		}

		public virtual void Disable()
		{
		}
	}
}