using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerScripts.Move
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform _groundCheckPosition;
        [SerializeField] private bool _isDebugEnable = true;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector2 _groundCheckSize;

        private bool _lastGroundedBool;
        public bool IsGrounded { get; private set; }

        public event UnityAction<bool> GroundedStateSwitched;

        private void Start()
        {
            GroundedStateSwitched?.Invoke(false);
        }

        private void FixedUpdate() => CheckGround();

        private void CheckGround()
        {
            IsGrounded = Physics2D.OverlapBox(_groundCheckPosition.position, _groundCheckSize, angle: 0, _groundLayer);

            TrySwitchGroundedBool();
        }

        private void TrySwitchGroundedBool()
        {
            if (_lastGroundedBool == IsGrounded)
                return;

            _lastGroundedBool = IsGrounded;
            GroundedStateSwitched?.Invoke(_lastGroundedBool);
        }

        private void OnDrawGizmos()
        {
            if (_isDebugEnable == true)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(_groundCheckPosition.position, _groundCheckSize);
            }
        }
    }
}