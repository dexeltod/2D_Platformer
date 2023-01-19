using Infrastructure.Services;
using PlayerScripts.TestStateMachine;

public class AnyToRunTransition : TestTransition<RunState>
	{
		private readonly IInputService _inputService;

		public AnyToRunTransition(StateService stateService, IInputService inputService) : base(stateService)
		{
			_inputService = inputService;
		}

		public override void Enable()
		{
			_inputService.VerticalButtonUsed += OnVerticalButtonUsed;
		}

		public override void Disable()
		{
			_inputService.VerticalButtonUsed -= OnVerticalButtonUsed;
		}

		private void OnVerticalButtonUsed(float direction) => 
			MoveNextState();
	}