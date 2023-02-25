using Game.PlayerScripts.StateMachine.States;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToChangeSceneTransition : StateTransition<ChangeSceneState>
	{
		private readonly PlayerSceneSwitcher _playerSceneSwitcher;

		public AnyToChangeSceneTransition(StateService stateService, PlayerSceneSwitcher playerSceneSwitcher) : base(stateService)
		{
			_playerSceneSwitcher = playerSceneSwitcher;
		}

		public override void Enable()
		{
			_playerSceneSwitcher.SceneSwitched += MoveNextState;
		}

		public override void Disable()
		{
			_playerSceneSwitcher.SceneSwitched -= MoveNextState;
		}
	}
}