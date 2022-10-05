using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsMovement : MonoBehaviour
{
    [SerializeField] protected float _groundCheckLineSize = 0.1f;
    [SerializeField] protected float _gravityModifier = 1f;
    [SerializeField] protected float _minGroundNormalY;

    protected const float ShellRadius = 0.01f;
    protected const float MinMoveDistance = 0.001f;

    protected Vector2 _targetVelocity;
    protected Vector2 _velocity;
    protected Vector2 _groundNormal;

    protected bool _isGrounded;
    protected bool _isJump;

    protected RaycastHit2D[] _hitBuffer = new RaycastHit2D[1];
    protected readonly RaycastHit2D[] _groundHits = new RaycastHit2D[16];
    protected List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    [SerializeField] protected ContactFilter2D _contactFilter;

    protected Rigidbody2D _rb2d;

    private void FixedUpdate()
    {
        _rb2d.gravityScale = _gravityModifier;
        _velocity += _rb2d.gravityScale * Time.deltaTime * Physics2D.gravity;
        _velocity.x = _targetVelocity.x;
        _isGrounded = false;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
        GroundCheck();
    }
    protected void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        
        if (distance > MinMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);
            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity -= projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        var a = _rb2d.position += move.normalized * distance;
        Debug.DrawLine(transform.position, a);
    }
    protected void GroundCheck()
    {
        int collisionsCount = _rb2d.Cast(-transform.up, _contactFilter, _groundHits, _groundCheckLineSize);

        if (collisionsCount >= 1)
        {
            _isGrounded = true;
        }

        else
        {
            _isGrounded = false;
        }
    }
}