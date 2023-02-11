using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;
using Game.PlayerScripts.Weapons;

namespace Game.PlayerScripts.StateMachine.Transitions.AttackTo
{
	public class AttackToFallTransition : StateTransition<FallState>
	{
		private readonly AbstractWeapon _abstractWeapon;
		private readonly PhysicsMovement _physicsMovement;

		public AttackToFallTransition(StateService stateService, AbstractWeapon abstractWeapon, PhysicsMovement physicsMovement) : base(stateService)
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
				MoveNextState();
		}
	}
}