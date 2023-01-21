namespace PlayerScripts.TestStateMachine
{
	public class AnyToDeadTransition : StateTransition<DeadState>
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