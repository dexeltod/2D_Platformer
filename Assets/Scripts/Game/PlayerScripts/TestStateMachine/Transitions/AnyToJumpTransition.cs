﻿using Infrastructure.Services;

namespace PlayerScripts.TestStateMachine
{
	public class AnyToJumpTransition : StateTransition<JumpState>
	{
		private readonly IInputService _inputService;
		private readonly GroundChecker _groundChecker;

		public AnyToJumpTransition(StateService stateService, IInputService inputService, GroundChecker groundChecker) :
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