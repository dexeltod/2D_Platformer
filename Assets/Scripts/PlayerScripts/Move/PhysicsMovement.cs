using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsMovement : MonoBehaviour
{
    protected const float ShellRadius = 0.01f;
    protected const float MinMoveDistance = 0.001f;

    [SerializeField] protected float _groundCheckLineSize = 0.1f;
    [SerializeField] protected float _gravityModifier = 1f;
    [SerializeField] protected float _minGroundNormalY;
    [SerializeField] protected ContactFilter2D ContactFilter;

    protected Vector2 TargetVelocity;
    protected Vector2 Velocity;
    protected Vector2 GroundNormal;

    protected bool IsGrounded;
    protected bool IsJump;

    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[1];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);
    protected readonly RaycastHit2D[] GroundHits = new RaycastHit2D[16];

    protected Rigidbody2D _rb2d;   

    protected void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rb2d.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);
            HitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                Vector2 currentNormal = HitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    IsGrounded = true;

                    if (yMovement)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);

                if (projection < 0)
                {
                    Velocity -= projection * currentNormal;
                }

                float modifiedDistance = HitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        var direction = _rb2d.position += move.normalized * distance;
        Debug.DrawLine(transform.position, direction);
    }

    protected void GroundCheck()
    {
        int collisionsCount = _rb2d.Cast(-transform.up, ContactFilter, GroundHits, _groundCheckLineSize);

        if (collisionsCount >= 1)
            IsGrounded = true;
        else
            IsGrounded = false;
    }

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb2d.gravityScale = _gravityModifier;
        Velocity += _rb2d.gravityScale * Time.deltaTime * Physics2D.gravity;
        Velocity.x = TargetVelocity.x;
        IsGrounded = false;
        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new(GroundNormal.y, -GroundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
        GroundCheck();
    }
}