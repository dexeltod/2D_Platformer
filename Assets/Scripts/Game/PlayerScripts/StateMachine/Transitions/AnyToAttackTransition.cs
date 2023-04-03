using Game.PlayerScripts.Move;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Game.PlayerScripts.StateMachine.Transitions
{
	public class AnyToAttackTransition : StateTransition<States.AttackState>
	{
		private readonly IInputService _inputService;

		public AnyToAttackTransition(StateService stateService, IInputService inputService, GroundChecker groundChecker)
			: base(stateService)
		{
			_inputService = inputService;
			_inputService.AttackButtonUsed += MoveNextState;
		}

		~AnyToAttackTransition()
		{
			_inputService.AttackButtonUsed -= MoveNextState;
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}
	}
}