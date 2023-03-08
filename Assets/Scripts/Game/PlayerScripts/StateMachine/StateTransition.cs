using System;

namespace Game.PlayerScripts.StateMachine
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

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}
	}
}