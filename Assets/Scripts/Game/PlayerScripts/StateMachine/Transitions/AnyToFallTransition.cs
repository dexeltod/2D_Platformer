namespace Game.PlayerScripts.TestStateMachine.Transitions
{
	public class AnyToFallTransition : StateTransition<States.FallState>
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
			_groundChecker.GroundedStateSwitched += OnGroundedChanged;
		}

		~AnyToFallTransition()
		{
			_groundChecker.GroundedStateSwitched -= OnGroundedChanged;
			_physicsMovement.Fallen -= OnFall;
		}

		public override void Enable()
		{
			base.Enable();
		}

		public override void Disable()
		{
			base.Disable();
		}

		private void OnGroundedChanged(bool isGrounded)
		{
			_isGrounded = isGrounded;
		}
		
		private void OnFall(bool isFall)
		{
			if (isFall == true && _isGrounded == false)
				MoveNextState();
		}
	}
}