
using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class AnyToIdleTransition : StateTransition<IdleState>
	{
		private readonly IInputService _inputService;
		private readonly PhysicsMovement _physicsMovement;
		private readonly GroundChecker _groundChecker;

		public AnyToIdleTransition(StateService stateService, IInputService inputService, PhysicsMovement physicsMovement, GroundChecker groundChecker) : base(stateService)
		{
			_inputService = inputService;
			_physicsMovement = physicsMovement;
			_groundChecker = groundChecker;
			_groundChecker.GroundedStateSwitched += OnGrounded;
		}

		~AnyToIdleTransition()
		{
			_groundChecker.GroundedStateSwitched -= OnGrounded;
		}

		public override void Enable()
		{
			_inputService.VerticalButtonCanceled += OnStay;
		}

		public override void Disable()
		{
			_inputService.VerticalButtonCanceled -= OnStay;
			
		}

		private void OnStay() => 
			MoveNextState();

		private void OnGrounded(bool isGrounded)
		{
			if (isGrounded == true && _physicsMovement.MovementDirection == Vector2.zero) 
				MoveNextState();
		}
	}
}