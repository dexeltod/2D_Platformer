﻿using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Infrastructure.Services;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToJumpTransition : StateTransition<JumpState>
	{
		private readonly IInputService _inputService;
		private readonly GroundChecker _groundChecker;

		public AnyToJumpTransition(StateService stateService, IInputService inputService,
			GroundChecker groundChecker) :
			base(stateService)
		{
			_inputService = inputService;
			_groundChecker = groundChecker;
			
		}

		~AnyToJumpTransition()
		{
			
		}

		public override void Enable()
		{
			_inputService.JumpButtonUsed += TryJump;
		}

		public override void Disable()
		{
			_inputService.JumpButtonUsed -= TryJump;
		}

		private void TryJump()
		{
			if (_groundChecker.IsGrounded == true)
				MoveNextState();
		}
	}
}