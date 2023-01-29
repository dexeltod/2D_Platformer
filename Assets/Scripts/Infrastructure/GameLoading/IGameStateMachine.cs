using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
	}
}