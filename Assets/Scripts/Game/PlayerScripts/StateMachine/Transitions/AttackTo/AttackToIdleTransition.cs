namespace Game.PlayerScripts.StateMachine.Transitions.AttackTo
{
	public class AttackToIdleTransition : StateTransition<States.RunState>
	{
		private readonly Weapons.AbstractWeapon _abstractWeapon;
		private readonly Move.PhysicsMovement _physicsMovement;

		private States.AttackState _attackState;

		public AttackToIdleTransition(StateService stateService, Weapons.AbstractWeapon abstractWeapon,
			Move.PhysicsMovement physicsMovement) : base(stateService)
		{
			_abstractWeapon = abstractWeapon;
			_physicsMovement = physicsMovement;
		}

		public override void Enable()
		{
			_abstractWeapon.AttackAnimationEnded += ChangeState;
		}

		public override void Disable()
		{
			_abstractWeapon.AttackAnimationEnded -= ChangeState;
		}

		private void ChangeState()
		{
			if (_physicsMovement.MovementDirection.x == 0 && _physicsMovement.IsGrounded)
			{
				MoveNextState();
			}
		}
	}
}