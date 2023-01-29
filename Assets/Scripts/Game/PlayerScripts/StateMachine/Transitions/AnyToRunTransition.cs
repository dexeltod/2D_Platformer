﻿using Infrastructure.Services;

namespace Game.PlayerScripts.TestStateMachine.Transitions
{
    public class AnyToRunTransition : StateTransition<States.RunState>
    {
        private readonly IInputService _inputService;
        private readonly PhysicsMovement _physicsMovement;
        private readonly GroundChecker _groundChecker;

        public AnyToRunTransition(StateService stateService, IInputService inputService, PhysicsMovement physicsMovement,
            GroundChecker groundChecker) :
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