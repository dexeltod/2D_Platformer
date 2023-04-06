using Game.Sound.Music;
using Infrastructure.GameLoading;
using Infrastructure.Services;
using View.UI_Scripts.Curtain;

namespace Infrastructure
{
    public class Game
    {
	    private readonly MusicSetter _musicSetter;
	    public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, MusicSetter musicSetter)
        {
	        _musicSetter = musicSetter;
	        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container, _musicSetter);
        }
    }
}