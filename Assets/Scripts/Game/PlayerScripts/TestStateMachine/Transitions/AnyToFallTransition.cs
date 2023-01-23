namespace PlayerScripts.TestStateMachine
{
	public class AnyToFallTransition : StateTransition<FallState>
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly GroundChecker _groundChecker;
		private bool _isGrounded;

		public AnyToFallTransition(StateService stateService, PhysicsMovement physicsMovement,
			GroundChecker groundChecker) : base(stateService)
		{
			_physicsMovement = physicsMovement;
			_groundChecker = groundChecker;
			_physicsMovement.Fallen += OnFall;
		}

		~AnyToFallTransition()
		{
			_physicsMovement.Fallen -= OnFall;
		}

		public override void Enable()
		{
			base.Enable();
			_groundChecker.GroundedStateSwitched += OnFall;
		}

		public override void Disable()
		{
			base.Disable();

			_groundChecker.GroundedStateSwitched -= OnFall;
		}

		private void OnFall(bool isFall)
		{
			if (isFall == true && _isGrounded == false)
				MoveNextState();
		}
	}
}