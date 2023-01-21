using Infrastructure.Services;

namespace PlayerScripts.TestStateMachine{
	public class AnyToAttackTransition : StateTransition<AttackState>
	{
		private readonly IInputService _inputService;

		public AnyToAttackTransition(StateService stateService, IInputService inputService, GroundChecker groundChecker) : base(stateService)
		{
			_inputService = inputService;
		}

		public override void Enable()
		{
			_inputService.AttackButtonUsed += MoveNextState;
		}

		public override void Disable()
		{
			_inputService.AttackButtonUsed -= MoveNextState;
		}

		
	}
}
