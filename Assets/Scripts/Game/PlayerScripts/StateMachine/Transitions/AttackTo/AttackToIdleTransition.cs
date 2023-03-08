using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Game.PlayerScripts.Weapons;

namespace Game.PlayerScripts.StateMachine.Transitions.AttackTo
{
	public class AttackToIdleTransition : StateTransition<IdleState>
	{
		private readonly AbstractWeapon _abstractWeapon;
		private readonly PhysicsMovement _physicsMovement;

		private AttackState _attackState;

		public AttackToIdleTransition(StateService stateService, AbstractWeapon abstractWeapon,
			PhysicsMovement physicsMovement) : base(stateService)
		{
			_abstractWeapon = abstractWeapon;
			_physicsMovement = physicsMovement;
			_abstractWeapon.AttackAnimationEnded += ChangeState;
		}

		~AttackToIdleTransition()
		{
			_abstractWeapon.AttackAnimationEnded -= ChangeState;
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
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