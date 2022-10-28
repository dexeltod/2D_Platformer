using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyObserve : MonoBehaviour
{
    [SerializeField] private PlayerHealth _enemyPlayer;
    [SerializeField] private DataEntityVisibility _entityVisibility;

    [SerializeField] private Transform _wallCheckTransform;
    [SerializeField] private Transform _ledgeCheckTransform;
    [SerializeField] private Transform _eyePosition;

    [Header("Debug")] [SerializeField] private bool _isEnableGizmos;
    [SerializeField] private bool _isDetectEnemy;

    private SpriteRenderer _sprite;
    protected float AngleRight = 90f;

    public int FacingDirection { get; private set; }
    public float AngleFacingDirection { get; private set; }
    public float DistanceBetweenEnemy { get; private set; }

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        FacingDirection = 1;
    }

    private void FixedUpdate()
    {
        IsSeeEnemy();
    }

    public bool CheckColliderHorizontal()
    {
        return Physics2D.Raycast(_wallCheckTransform.position, Vector2.right * FacingDirection,
            _entityVisibility.WallCheckDistance, _entityVisibility.WhatIsTouched);
    }

    public bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheckTransform.position, Vector2.down,
            _entityVisibility.LedgeCheckDistance, _entityVisibility.WhatIsGround);
    }

    public bool IsSeeEnemy()
    {
        SetPositionAboutPlayer();

        bool isLookAtEnemy = AngleFacingDirection < _entityVisibility.AngleOfVisibility &&
                             DistanceBetweenEnemy <= _entityVisibility.VisibilityRange;

        if (isLookAtEnemy)
            return _isDetectEnemy = true;

        _isDetectEnemy = false;
        return false;
    }

    public void RotateFacingDirection()
    {
        const float RotationValue = -1f;

        FacingDirection *= -1;

        var localPosition = _ledgeCheckTransform.localPosition;
        localPosition = new Vector2(localPosition.x * RotationValue, localPosition.y);
        _ledgeCheckTransform.localPosition = localPosition;

        _sprite.flipX = _sprite.flipX != true;
    }

    private void SetPositionAboutPlayer()
    {
        var position = _eyePosition.position;
        var eyePositionPosition = _enemyPlayer.transform.position;
        Vector2 targetDirection = eyePositionPosition - position;
        Vector2 forward = _eyePosition.right;

        DistanceBetweenEnemy = Vector2.Distance(position, eyePositionPosition);
        AngleFacingDirection = Vector2.Angle(targetDirection, forward);
    }

    private void OnDrawGizmos()
    {
        if (_isEnableGizmos != true)
            return;

        Vector3 wallCheckDirection = _wallCheckTransform.position + (Vector3)(_entityVisibility.WallCheckDistance
                                                                              * FacingDirection * Vector2.right);

        Vector2 ledgeCheckDirection = _ledgeCheckTransform.position +
                                      (Vector3)Vector2.down * _entityVisibility.LedgeCheckDistance;

        var eyePosition = _eyePosition.position;
        
        Vector3 eyeFirstLineOfAngle = new Vector2(eyePosition.x
                                                  + _entityVisibility.VisibilityRange * FacingDirection,
            eyePosition.y + _entityVisibility.AngleOfVisibility / 10);

        Vector3 eyeSecondLineOfAngle = new Vector2(
            eyePosition.x + _entityVisibility.VisibilityRange * FacingDirection,
            eyePosition.y - _entityVisibility.AngleOfVisibility / 10);

        Gizmos.DrawLine(_wallCheckTransform.position, wallCheckDirection);
        Gizmos.DrawLine(_ledgeCheckTransform.position, ledgeCheckDirection);

        Gizmos.DrawLine(eyePosition, eyeFirstLineOfAngle);
        Gizmos.DrawLine(eyePosition, eyeSecondLineOfAngle);
    }
}