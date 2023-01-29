using Infrastructure.Services;

namespace Game.PlayerScripts.StateMachine.Transitions
{
    public class AnyToRunTransition : StateTransition<States.RunState>
    {
        private readonly IInputService _inputService;
        private readonly Move.PhysicsMovement _physicsMovement;
        private readonly Move.GroundChecker _groundChecker;

        public AnyToRunTransition(StateService stateService, IInputService inputService, Move.PhysicsMovement physicsMovement,
            Move.GroundChecker groundChecker) :
            base(stateService)
        {
            _inputService = inputService;
            _physicsMovement = physicsMovement;
            _groundChecker = groundChecker;
            _groundChecker.GroundedStateSwitched += OnGroundedAndRun;
            _inputService.VerticalButtonUsed += OnVerticalButtonUsed;
        }

        ~AnyToRunTransition()
        {
            _groundChecker.GroundedStateSwitched -= OnGroundedAndRun;
            _inputService.VerticalButtonUsed -= OnVerticalButtonUsed;
        }

        public override void Enable()
        {
            OnGroundedAndRun(_groundChecker.IsGrounded);
        }

        public override void Disable()
        {
        }

        private void OnGroundedAndRun(bool isGrounded)
        {
            if (isGrounded && _physicsMovement.MovementDirection.x != 0)
                MoveNextState();
        }

        private void OnVerticalButtonUsed(float direction)
        {
            if (_physicsMovement.IsGrounded == true)
                MoveNextState();
        }
    }
}