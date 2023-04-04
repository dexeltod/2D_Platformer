using Game.SceneConfigs;
using Infrastructure.States;

namespace Infrastructure.Services.Interfaces
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;

		void Enter<TState, TPayload, T>(TPayload payload, string music, bool isLevelNameIsStopMusicBetweenScenes)
			where TState : class, IPayloadState<TPayload>;
	}
}