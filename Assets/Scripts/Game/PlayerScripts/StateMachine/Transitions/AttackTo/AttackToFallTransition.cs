using PlayerScripts.Weapons;

namespace Game.PlayerScripts.TestStateMachine.Transitions.AttackTo
{
	public class AttackToFallTransition : StateTransition<States.RunState>
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
			{
				MoveNextState();
			}
		}
	}
}