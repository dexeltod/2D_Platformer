using Infrastructure.Services;

namespace PlayerScripts.TestStateMachine
{
	public class AnyToJumpTransition : StateTransition<JumpState>
	{
		private readonly IInputService _inputService;

		public AnyToJumpTransition(StateService stateService, IInputService inputService, GroundChecker groundChecker) : base(stateService)
		{
			_inputService = inputService;
			
		}

		public override void Enable()
		{
			base.Enable();
			_inputService.JumpButtonUsed += MoveNextState;
		}

		public override void Disable()
		{
			base.Disable();
			_inputService.JumpButtonUsed -= MoveNextState;
		}
	}
}