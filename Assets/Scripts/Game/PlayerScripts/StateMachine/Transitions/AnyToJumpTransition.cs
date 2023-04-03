using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToJumpTransition : StateTransition<JumpState>
	{
		private readonly IInputService _inputService;
		private readonly GroundChecker _groundChecker;
		private readonly PhysicsMovement _physicsMovement;

		public AnyToJumpTransition(StateService stateService, IInputService inputService,
			GroundChecker groundChecker, PhysicsMovement physicsMovement) :
			base(stateService)
		{
			_inputService = inputService;
			_groundChecker = groundChecker;
			_physicsMovement = physicsMovement;
		}

		public override void OnEnable()
		{
			_inputService.JumpButtonUsed += TryJump;
		}

		public override void OnDisable()
		{
			_inputService.JumpButtonUsed -= TryJump;
		}

		private void TryJump()
		{
			if (_physicsMovement.IsGrounded == false && _physicsMovement.IsTouchWall)
			{
				MoveNextState();
				return;
			}
			
			if (_groundChecker.IsGrounded == true)
				MoveNextState();
		}
	}
}