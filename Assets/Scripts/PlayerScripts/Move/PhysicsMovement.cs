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
    protected Vector2 GroundNormal;

    protected bool IsGrounded;
    protected bool IsJump;

    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[1];
    protected List<RaycastHit2D> HitBufferList = new(16);
    protected readonly RaycastHit2D[] GroundHits = new RaycastHit2D[16];

    protected Rigidbody2D Rigidbody2D;   

    protected void Move(Vector2 move, bool yMovement)
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
                GetCountedDistance(distance, yMovement, i);
            }
        }

        var direction = Rigidbody2D.position += move.normalized * distance;
        Debug.DrawLine(transform.position, direction);
    }

    private float GetCountedDistance(float distance, bool yMovement, int hitIndex)
    {
        Vector2 currentNormal = HitBufferList[hitIndex].normal;

        if (currentNormal.y > MinGroundNormalY)
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

        float modifiedDistance = HitBufferList[hitIndex].distance - ShellRadius;
        distance = modifiedDistance < distance ? modifiedDistance : distance;

        return distance;
    }

    protected void GroundCheck()
    {
        int collisionsCount = Rigidbody2D.Cast(-transform.up, ContactFilter, GroundHits, GroundCheckLineSize);

        if (collisionsCount >= 1)
            IsGrounded = true;
        else
            IsGrounded = false;
    }

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
        Vector2 moveAlongGround = new(GroundNormal.y, -GroundNormal.x);

        Vector2 moveDiraction = moveAlongGround * deltaPosition.x;
        Move(moveDiraction, false);
        moveDiraction = Vector2.up * deltaPosition.y;

        Move(moveDiraction, true);
        GroundCheck();
    }
}