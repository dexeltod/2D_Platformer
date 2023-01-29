using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToIdleTransition : StateTransition<States.IdleState>
	{
		private readonly IInputService _inputService;
		private readonly Move.PhysicsMovement _physicsMovement;
		private readonly Move.GroundChecker _groundChecker;

		public AnyToIdleTransition(StateService stateService, IInputService inputService,
			Move.PhysicsMovement physicsMovement, Move.GroundChecker groundChecker) : base(stateService)
		{
			_inputService = inputService;
			_physicsMovement = physicsMovement;
			_groundChecker = groundChecker;
			_groundChecker.GroundedStateSwitched += OnGrounded;
			_inputService.VerticalButtonCanceled += OnStay;
		}

		~AnyToIdleTransition()
		{
			_groundChecker.GroundedStateSwitched -= OnGrounded;
			_inputService.VerticalButtonCanceled -= OnStay;
		}

		public override void Enable()
		{
		}

		public override void Disable()
		{
		}

		private void OnStay()
		{
			if (_groundChecker.IsGrounded == true)
				MoveNextState();
		}

		private void OnGrounded(bool isGrounded)
		{
			if (isGrounded == true && _physicsMovement.MovementDirection == Vector2.zero)
				MoveNextState();
		}
	}
}