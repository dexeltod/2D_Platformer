using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
		void Enter<TState, TPayload, T>(TPayload payload, bool isLevelNameIsStopMusicBetweenScenes, string musicName) where TState : class, IPayloadState<TPayload>;
	}
}