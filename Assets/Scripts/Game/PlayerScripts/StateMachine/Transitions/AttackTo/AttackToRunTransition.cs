using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Game.PlayerScripts.Weapons;

namespace Game.PlayerScripts.StateMachine.Transitions.AttackTo
{
	public class AttackToRunTransition : StateTransition<RunState>
	{
		private readonly AbstractWeapon _abstractWeapon;
		private readonly PhysicsMovement _physicsMovement;

		public AttackToRunTransition(StateService stateService, AbstractWeapon abstractWeapon, PhysicsMovement physicsMovement) : base(stateService)
		{
			_abstractWeapon = abstractWeapon;
			_physicsMovement = physicsMovement;
			_abstractWeapon.AttackAnimationEnded += ChangeState;
		}

		~AttackToRunTransition()
		{
			_abstractWeapon.AttackAnimationEnded -= ChangeState;
		}
		
		private void ChangeState()
		{
			if (_physicsMovement.MovementDirection.x != 0 && _physicsMovement.IsGrounded == true)
			{
				MoveNextState();
			}
		}
	}
}