using Infrastructure.Services;

namespace PlayerScripts.TestStateMachine
{
	public class AnyToIdleTransition : TestTransition<IdleState>
	{
		private readonly IInputService _inputService;

		public AnyToIdleTransition(StateService stateService, IInputService inputService) : base(stateService)
		{
			_inputService = inputService;
		}

		public override void Enable()
		{
			_inputService.VerticalButtonCanceled += MoveNextState;
		}

		public override void Disable()
		{
			_inputService.VerticalButtonCanceled -= MoveNextState;
		}
	}
}