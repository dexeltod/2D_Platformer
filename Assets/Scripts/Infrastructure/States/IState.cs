namespace Infrastructure.States
{
	public interface IState : IExitState
	{
		void Enter();
	}

	public interface IExitState
	{
		void Exit();
	}

	public interface IPayloadState<TPayload> : IExitState
	{
		void Enter(TPayload payload);
	}
}