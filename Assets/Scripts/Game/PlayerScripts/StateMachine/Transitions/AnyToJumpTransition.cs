﻿using Infrastructure.Services;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToJumpTransition : StateTransition<States.JumpState>
	{
		private readonly IInputService _inputService;
		private readonly Move.GroundChecker _groundChecker;

		public AnyToJumpTransition(StateService stateService, IInputService inputService, Move.GroundChecker groundChecker) :
			base(stateService)
		{
			_inputService = inputService;
			_groundChecker = groundChecker;
		}

		public override void Enable()
		{
			base.Enable();
			_inputService.JumpButtonUsed += TryJump;
		}

		public override void Disable()
		{
			base.Disable();
			_inputService.JumpButtonUsed -= TryJump;
		}

		private void TryJump()
		{
			if (_groundChecker.IsGrounded == true) 
				MoveNextState();
		}
	}
}