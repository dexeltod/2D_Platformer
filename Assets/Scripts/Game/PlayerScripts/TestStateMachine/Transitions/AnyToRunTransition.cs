using Infrastructure.Services;
using PlayerScripts.TestStateMachine;

public class AnyToRunTransition : StateTransition<RunState>
{
	private readonly IInputService _inputService;
	private readonly PhysicsMovement _physicsMovement;
	private readonly GroundChecker _groundChecker;
	private float _direction;

	public AnyToRunTransition(StateService stateService, IInputService inputService, PhysicsMovement physicsMovement, GroundChecker groundChecker) :
		base(stateService)
	{
		_inputService = inputService;
		_physicsMovement = physicsMovement;
		_groundChecker = groundChecker;
	}

	public override void Enable()
	{
		
		_groundChecker.GroundedStateSwitched += OnGroundedAndRun;
		_inputService.VerticalButtonUsed += OnVerticalButtonUsed;
	}

	public override void Disable()
	{
		_groundChecker.GroundedStateSwitched -= OnGroundedAndRun;
		_inputService.VerticalButtonUsed -= OnVerticalButtonUsed;
	}

	private void OnGroundedAndRun(bool isGrounded)
	{
		if (isGrounded && _direction != 0)
			MoveNextState();
	}

	private void OnVerticalButtonUsed(float direction)
	{
		if (_physicsMovement.IsGrounded == true)
			MoveNextState();
	}
}