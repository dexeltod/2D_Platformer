namespace Game.PlayerScripts.TestStateMachine.Transitions
{
	public class AnyToDeadTransition : StateTransition<States.DeadState>
	{
		public AnyToDeadTransition(StateService stateService) : base(stateService)
		{
		}

		public override void Enable()
		{
		}

		public override void Disable()
		{
		}

		
	}
}