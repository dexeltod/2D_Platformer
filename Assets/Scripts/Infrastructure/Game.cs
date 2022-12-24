using Infrastructure;
using Infrastructure.Services;
using UI_Scripts.Curtain;

public class Game
{
	public static IInputService InputService;
	public readonly GameStateMachine StateMachine;

	public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
	{
		StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container);
	}
}