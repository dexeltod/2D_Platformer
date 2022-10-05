using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public int FacingDirection { get; protected set; }
    public float AngleFacingDirection { get; protected set; }
    public float DistanceBetweenEnemy { get; protected set; }

    protected float AngleRight = 90f;

    [SerializeField] protected PlayerEntity _enemyPlayer;
    [SerializeField] protected D_EntityVisibility EntityVisibility;
    [SerializeField] protected Transform _wallCheckTransform;
    [SerializeField] protected Transform _ledgeCheckTransform;
    [SerializeField] protected Transform _eyePosition;

    [Header("Debug")]
    [SerializeField] private bool _debugGizmos;
    [SerializeField] protected bool _isDetectEnemy;

    private void Start()
    {
        FacingDirection = 1;
    }

    private void FixedUpdate()
    {
        IsSeeEnemy();
    }

    public virtual bool CheckColliderHorizontal()
    {
        return Physics2D.Raycast(_wallCheckTransform.position, Vector2.right * FacingDirection, EntityVisibility.WallCheckDistance, EntityVisibility.WhatIsTouched);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheckTransform.position, Vector2.down, EntityVisibility.LedgeCheckDistance, EntityVisibility.WhatIsGround);
    }    

    public virtual bool IsSeeEnemy()
    {
        CheckDistanceBetweenTarget();

        if (AngleFacingDirection < EntityVisibility.AngleOfVisibility && DistanceBetweenEnemy <= EntityVisibility.VisibilityRange)
        {
            _isDetectEnemy = true;
            return true;
        }
        _isDetectEnemy = false;
        return false;
    }
    public void RotateFacingDirection()
    {
        FacingDirection *= -1;
    }

    private void CheckDistanceBetweenTarget()
    {
        Vector2 targetDirection = _enemyPlayer.EyePosition.position - _eyePosition.position;
        Vector2 forward = _eyePosition.right;

        DistanceBetweenEnemy = Vector2.Distance(_eyePosition.position, _enemyPlayer.EyePosition.position);
        AngleFacingDirection = Vector2.Angle(targetDirection, forward);
    }    

    public virtual void OnDrawGizmos()
    {
        if (_debugGizmos)
        {
            var direction = FacingDirection;

            Gizmos.DrawLine(_wallCheckTransform.position, _wallCheckTransform.position + (Vector3)(Vector2.right * EntityVisibility.WallCheckDistance * FacingDirection));
            Gizmos.DrawLine(_ledgeCheckTransform.position, _ledgeCheckTransform.position + (Vector3)(Vector2.down * EntityVisibility.LedgeCheckDistance));

            Gizmos.DrawLine(_eyePosition.position, new Vector2(_eyePosition.position.x + EntityVisibility.VisibilityRange * direction, (_eyePosition.position.y + EntityVisibility.AngleOfVisibility / 10)));
            Gizmos.DrawLine(_eyePosition.position, new Vector2(_eyePosition.position.x + EntityVisibility.VisibilityRange * direction, (_eyePosition.position.y - EntityVisibility.AngleOfVisibility / 10)));

            Gizmos.DrawCube(transform.position, new Vector2(0.1f, 0.1f));
        }
    }
}
