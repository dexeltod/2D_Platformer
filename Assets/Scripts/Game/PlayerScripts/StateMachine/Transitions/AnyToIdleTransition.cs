using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToIdleTransition : StateTransition<IdleState>
	{
		private readonly IInputService _inputService;
		private readonly PhysicsMovement _physicsMovement;
		private readonly GroundChecker _groundChecker;

		public AnyToIdleTransition(StateService stateService, IInputService inputService,
			PhysicsMovement physicsMovement, GroundChecker groundChecker) : base(stateService)
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