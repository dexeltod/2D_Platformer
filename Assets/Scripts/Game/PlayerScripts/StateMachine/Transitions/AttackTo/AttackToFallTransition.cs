namespace Game.PlayerScripts.StateMachine.Transitions.AttackTo
{
	public class AttackToFallTransition : StateTransition<States.RunState>
	{
		private readonly Weapons.AbstractWeapon _abstractWeapon;
		private readonly Move.PhysicsMovement _physicsMovement;

		public AttackToFallTransition(StateService stateService, Weapons.AbstractWeapon abstractWeapon, Move.PhysicsMovement physicsMovement) : base(stateService)
		{
			_abstractWeapon = abstractWeapon;
			_physicsMovement = physicsMovement;
			_abstractWeapon.AttackAnimationEnded += ChangeState;
		}

		~AttackToFallTransition()
		{
			_abstractWeapon.AttackAnimationEnded -= ChangeState;
		}
		
		public override void Enable()
		{
		}

		public override void Disable()
		{
			
		}

		private void ChangeState()
		{
			if (_physicsMovement.IsGrounded == false)
			{
				MoveNextState();
			}
		}
	}
}