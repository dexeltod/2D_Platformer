using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    [SerializeField] private EnemyLookAround _enemyLook;
    [SerializeField] private DataEntity _dataEntity;

    private Animator _animator;
    private Rigidbody2D _rb2d;
    private string _animBoolName = "isWalk";
    private Vector2 _moveSpeed;

    private void Awake()
    {
        _rb2d = GetComponentInParent<Rigidbody2D>();    
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        _moveSpeed = new Vector2(_dataEntity.MoveSpeed, transform.position.y);
        _animator.SetBool(_animBoolName, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(_animBoolName, false);
    }

    private void FixedUpdate()
    {
        MoveVertical();
    }

    private void MoveVertical()
    {
        if(_enemyLook.CheckLedge() || !_enemyLook.CheckColliderHorizontal())
        {
            _rb2d.velocity = new Vector3(_moveSpeed.x * _enemyLook.FacingDirection, _rb2d.velocity.y);
        }
    }

    public void StopMoveVertical()
    {
        _rb2d.velocity = new Vector3(_rb2d.velocity.x * 0, _rb2d.velocity.y);
    }
}
