namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToDeadTransition : StateTransition<States.DeadState>
	{
		public AnyToDeadTransition(StateService stateService) : base(stateService)
		{
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}

		
	}
}