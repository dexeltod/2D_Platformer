using Game.PlayerScripts.Move;
using Infrastructure.Services;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToJumpTransition : StateTransition<States.JumpState>
	{
		private readonly IInputService _inputService;
		private readonly GroundChecker _groundChecker;

		public AnyToJumpTransition(StateService stateService, IInputService inputService,
			Move.GroundChecker groundChecker) :
			base(stateService)
		{
			_inputService = inputService;
			_groundChecker = groundChecker;
			_inputService.JumpButtonUsed += TryJump;
		}

		~AnyToJumpTransition()
		{
			_inputService.JumpButtonUsed -= TryJump;
		}

		public override void Enable()
		{
			base.Enable();
		}

		public override void Disable()
		{
			base.Disable();
		}

		private void TryJump()
		{
			if (_groundChecker.IsGrounded == true)
				MoveNextState();
		}
	}
}