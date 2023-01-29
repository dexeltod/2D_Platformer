using PlayerScripts.Weapons;

namespace Game.PlayerScripts.TestStateMachine.Transitions.AttackTo
{
	public class AttackToIdleTransition : StateTransition<States.RunState>
	{
		private readonly AbstractWeapon _abstractWeapon;
		private readonly PhysicsMovement _physicsMovement;

		private States.AttackState _attackState;

		public AttackToIdleTransition(StateService stateService, AbstractWeapon abstractWeapon,
			PhysicsMovement physicsMovement) : base(stateService)
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