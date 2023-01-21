namespace PlayerScripts.TestStateMachine
{
	public class AnyToFallTransition : StateTransition<FallState>
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly GroundChecker _groundChecker;

		public AnyToFallTransition(StateService stateService, PhysicsMovement physicsMovement, GroundChecker groundChecker) : base(stateService)
		{
			_physicsMovement = physicsMovement;
			_groundChecker = groundChecker;
		}

		public override void Enable()
		{
			base.Enable();
			_physicsMovement.Fallen += OnFall;
			_groundChecker.GroundedStateSwitched += OnFall;
		}

		public override void Disable()
		{
			base.Disable();
			_physicsMovement.Fallen -= OnFall;
			_groundChecker.GroundedStateSwitched -= OnFall;
		}

		private void OnFall(bool isFall)
		{
			if (isFall)
				MoveNextState();
		}
	}
}