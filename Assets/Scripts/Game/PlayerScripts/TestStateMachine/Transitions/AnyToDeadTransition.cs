namespace PlayerScripts.TestStateMachine
{
	public class AnyToDeadTransition : TestTransition<DeadState>
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