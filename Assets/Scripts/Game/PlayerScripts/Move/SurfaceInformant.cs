using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerScripts.Move
{
    public class SurfaceInformant : MonoBehaviour
    {
        [SerializeField] private bool _isDebug;
        [SerializeField, Range(0, 180)] private float _maxSlopeAngle = 57;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _slopeCheckDistance = 1;

        private CapsuleCollider2D _capsuleCollider;
        private RaycastHit2D _hit;
	
        private Vector2 _normal;
        private Vector2 _capsuleColliderSize;
        private Vector2 _slopeNormalPerp;
        private Vector2 _centerPosition;

        private float _slopeSideAngle;
        private float _slopeDownAngle;
        private float _lastSlopeAngle;
        private float _moveDirectionX;

        private bool _isGlideLast;
        private bool _isGlide;
        private bool _isOnSlope;
        private bool _canWalkOnSlope;
        private bool _canWalkOnSlopeLast;

        public event UnityAction<bool> GlideStateSwitched;
        public event UnityAction<bool> Moves;

        private void Start()
        {
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            _capsuleColliderSize = _capsuleCollider.size;
        }

        public Vector2 GetProjectionAlongNormal(Vector2 direction)
        {
            CheckAngleSurface();
            SlopeCheckVertical();

            _centerPosition = (Vector2)transform.position + new Vector2(0.0f, _capsuleColliderSize.y / 2);

            _hit = Physics2D.Raycast(
                _centerPosition,
                Vector2.down,
                _slopeCheckDistance,
                _groundLayer);

            Vector2 directionAlongSurface = direction;

            if (_hit)
            {
                _normal = _hit.normal;
                _slopeNormalPerp = Vector2.Perpendicular(_hit.normal).normalized;
                directionAlongSurface.Set(-direction.x * _slopeNormalPerp.x, -direction.x * _slopeNormalPerp.y);
            }

            return directionAlongSurface;
        }

        private void CheckAngleSurface()
        {
            _isGlide = _lastSlopeAngle > _maxSlopeAngle;

            if (_isGlideLast == _isGlide)
                return;

            _isGlideLast = _isGlide;
            GlideStateSwitched?.Invoke(_isGlide);
        }

        private void SlopeCheckVertical()
        {
            RaycastHit2D hit = Physics2D.Raycast(_centerPosition, Vector2.down, _slopeCheckDistance, _groundLayer);

            if (hit)
            {
                _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
                _lastSlopeAngle = _slopeDownAngle;
            }

            if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
                _canWalkOnSlope = false;
            else
                _canWalkOnSlope = true;

            if (_canWalkOnSlope == _canWalkOnSlopeLast)
                return;

            _canWalkOnSlopeLast = _canWalkOnSlope;
            Moves?.Invoke(_canWalkOnSlope);
        }

        private void OnDrawGizmos()
        {
            if (_isDebug == true)
            {
                Gizmos.color = new Color(0.2f, 0.2f, 1f);
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_normal);
                Gizmos.color = new Color(1f, 0f, 0.6f);
                Gizmos.DrawLine(_hit.point, _hit.point + _slopeNormalPerp);
                Gizmos.color = new Color(0f, 1f, 0.99f);
                Gizmos.DrawLine(_centerPosition, _centerPosition + Vector2.down * _slopeCheckDistance);
                Gizmos.color = new Color(1f, 0.99f, 0f);
                Gizmos.DrawWireSphere(_centerPosition, 0.01f);
            }
        }
    }
}