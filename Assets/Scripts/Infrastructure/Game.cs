using Game.Sound.Music;
using Infrastructure.GameLoading;
using UI_Scripts.Curtain;

namespace Infrastructure
{
    public class Game
    {
	    private readonly SoundSetter _soundSetter;
	    public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, SoundSetter soundSetter)
        {
	        _soundSetter = soundSetter;
	        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container, _soundSetter);
        }
    }
}