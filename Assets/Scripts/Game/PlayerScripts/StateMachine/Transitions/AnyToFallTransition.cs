namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToFallTransition : StateTransition<States.FallState>
	{
		private readonly Move.PhysicsMovement _physicsMovement;
		private readonly Move.GroundChecker _groundChecker;
		private bool _isGrounded;

		public AnyToFallTransition(StateService stateService, Move.PhysicsMovement physicsMovement,
			Move.GroundChecker groundChecker) : base(stateService)
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