using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine.States;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToWallSlideTransition: StateTransition<WallSlideState>
	{
		private readonly WallCheckTrigger _wallCheckTrigger;
		private readonly GroundChecker _groundChecker;

		public AnyToWallSlideTransition(StateService stateService, WallCheckTrigger wallCheckTrigger, GroundChecker groundChecker) : base(stateService)
		{
			_wallCheckTrigger = wallCheckTrigger;
			_groundChecker = groundChecker;
		}

		public override void OnEnable()
		{
			_wallCheckTrigger.WallTouched += OnWallTouched;
		}
		public override void OnDisable()
		{
			_wallCheckTrigger.WallTouched -= OnWallTouched;
		}

		private void OnWallTouched(bool wallTouched)
		{
			if(_groundChecker.IsGrounded == false && wallTouched == true)
				MoveNextState();
		}
	}
}