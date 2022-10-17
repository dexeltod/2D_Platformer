using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsMovement : MonoBehaviour
{
    protected const float ShellRadius = 0.01f;
    protected const float MinMoveDistance = 0.001f;

    [SerializeField] protected float GroundCheckLineSize = 0.1f;
    [SerializeField] protected float GravityModifier = 1f;
    [SerializeField] protected float MinGroundNormalY;
    [SerializeField] protected ContactFilter2D ContactFilter;

    protected Vector2 TargetVelocity;
    protected Vector2 Velocity;

    protected bool IsGrounded;
    protected bool IsJump;

    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[1];
    protected List<RaycastHit2D> HitBufferList = new(16);
    protected readonly RaycastHit2D[] GroundHits = new RaycastHit2D[16];

    protected Rigidbody2D Rigidbody2D;
    private Vector2 _groundNormal;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.gravityScale = GravityModifier;

        Velocity += Rigidbody2D.gravityScale * Time.deltaTime * Physics2D.gravity;
        Velocity.x = TargetVelocity.x;

        IsGrounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new(_groundNormal.y, -_groundNormal.x);

        Vector2 moveDirection = moveAlongGround * deltaPosition.x;
        Move(moveDirection, false);
        moveDirection = Vector2.up * deltaPosition.y;

        Move(moveDirection, true);
        GroundCheck();
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = Rigidbody2D.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);
            HitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                SetCountedDistance(distance, yMovement, i);
            }
        }

        Rigidbody2D.position += move.normalized * distance;
    }

    private void SetCountedDistance(float distance, bool yMovement, int hitIndex)
    {
        Vector2 currentNormal = HitBufferList[hitIndex].normal;

        if (currentNormal.y > MinGroundNormalY)
        {
            IsGrounded = true;

            if (yMovement)
            {
                _groundNormal = currentNormal;
                currentNormal.x = 0;
            }
        }

        Debug.DrawLine(transform.position, _groundNormal + (Vector2)transform.position);
        float projection = Vector2.Dot(Velocity, currentNormal);

        if (projection < 0)
        {
            Velocity -= projection * currentNormal;
        }

        float modifiedDistance = HitBufferList[hitIndex].distance - ShellRadius;
        distance = modifiedDistance < distance ? modifiedDistance : distance;
    }

    private void GroundCheck()
    {
        int collisionsCount = Rigidbody2D.Cast(-transform.up, ContactFilter, GroundHits, GroundCheckLineSize);

        IsGrounded = collisionsCount >= 1;
    }
}