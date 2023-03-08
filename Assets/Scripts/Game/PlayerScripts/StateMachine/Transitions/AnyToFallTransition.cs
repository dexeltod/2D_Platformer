using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToFallTransition : StateTransition<FallState>
	{
		private readonly PhysicsMovement _physicsMovement;
		private bool _isGrounded;

		public AnyToFallTransition(StateService stateService, PhysicsMovement physicsMovement) : base(stateService)
		{
			_physicsMovement = physicsMovement;
		}

		public override void OnEnable()
		{
			_physicsMovement.Fallen += OnFall;
		}

		public override void OnDisable()
		{
			_physicsMovement.Fallen -= OnFall;
		}
		
		private void OnFall(bool isFall)
		{
			if (isFall == true && _physicsMovement.IsGrounded == false)
				MoveNextState();
		}
	}
}