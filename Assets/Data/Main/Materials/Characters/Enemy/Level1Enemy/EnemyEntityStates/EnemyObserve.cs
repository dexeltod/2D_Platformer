using DG.Tweening;
using UnityEngine;

public class EnemyObserve : MonoBehaviour
{
    [SerializeField] private PlayerEntity _enemyPlayer;
    [SerializeField] private D_EntityVisibility EntityVisibility;
    [SerializeField] private Transform _wallCheckTransform;
    [SerializeField] private Transform _ledgeCheckTransform;
    [SerializeField] private Transform _eyePosition;

    [Header("Debug")]
    [SerializeField] private bool _isEnableGizmos;
    [SerializeField] private bool _isDetectEnemy;

    private SpriteRenderer _sprite;
    protected float AngleRight = 90f;

    public int FacingDirection { get; protected set; }
    public float AngleFacingDirection { get; protected set; }
    public float DistanceBetweenEnemy { get; protected set; }

    public bool CheckColliderHorizontal()
    {
        return Physics2D.Raycast(_wallCheckTransform.position, Vector2.right * FacingDirection,
                                  EntityVisibility.WallCheckDistance, EntityVisibility.WhatIsTouched);
    }

    public bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheckTransform.position, Vector2.down,
                                  EntityVisibility.LedgeCheckDistance, EntityVisibility.WhatIsGround);
    }

    public bool IsSeeEnemy()
    {
        SetPositionByEnemy();

        bool isLookAtEnemy = AngleFacingDirection < EntityVisibility.AngleOfVisibility &&
                             DistanceBetweenEnemy <= EntityVisibility.VisibilityRange;

        if (isLookAtEnemy)
            return _isDetectEnemy = true;

        _isDetectEnemy = false;
        return false;
    }

    public void RotateFacingDirection()
    {
        Debug.Log("huy");
        FacingDirection *= -1;
        _ledgeCheckTransform.localPosition = new Vector2(_ledgeCheckTransform.localPosition.x * -1f, _ledgeCheckTransform.localPosition.y);

        if (_sprite.flipX == true)
            _sprite.flipX = false;
        else
            _sprite.flipX = true;
    }

    private void SetPositionByEnemy()
    {
        Vector2 targetDirection = _enemyPlayer.EyePosition.position - _eyePosition.position;
        Vector2 forward = _eyePosition.right;

        DistanceBetweenEnemy = Vector2.Distance(_eyePosition.position, _enemyPlayer.EyePosition.position);
        AngleFacingDirection = Vector2.Angle(targetDirection, forward);
    }

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        FacingDirection = 1;
    }

    private void FixedUpdate()
    {
        IsSeeEnemy();
    }

    private void OnDrawGizmos()
    {
        Vector3 wallCheckDirection = _wallCheckTransform.position + (Vector3)(EntityVisibility.WallCheckDistance
                * FacingDirection * Vector2.right);

        Vector2 ledgeCheckDirection = _ledgeCheckTransform.position + (Vector3)Vector2.down * EntityVisibility.LedgeCheckDistance;

        Vector3 eyeFirstLineOfAngle = new Vector2(_eyePosition.position.x
                            + EntityVisibility.VisibilityRange * FacingDirection,
                              _eyePosition.position.y + EntityVisibility.AngleOfVisibility / 10);

        Vector3 eyeSecondeLineOfAngle = new Vector2(_eyePosition.position.x + EntityVisibility.VisibilityRange * FacingDirection,
                            _eyePosition.position.y - EntityVisibility.AngleOfVisibility / 10);

        if (_isEnableGizmos == true)
        {
            Gizmos.DrawLine(_wallCheckTransform.position, wallCheckDirection);
            Gizmos.DrawLine(_ledgeCheckTransform.position, ledgeCheckDirection);

            Gizmos.DrawLine(_eyePosition.position, eyeFirstLineOfAngle);
            Gizmos.DrawLine(_eyePosition.position, eyeSecondeLineOfAngle);

            //Gizmos.DrawCube(transform.position, new Vector2(0.1f, 0.1f));
        }
    }
}
