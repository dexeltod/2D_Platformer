using UnityEngine;

[RequireComponent(typeof(InputSystemReader))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMoveController))]

public class PlayerAttack : MonoBehaviour
{
    [Header("Set target and damage")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _maxDistanceToAttack;
    [SerializeField] private float _attackDelay;
    [SerializeField] private LayerMask _layerMask;

    [Header("Set hand point")]
    [SerializeField] private Transform _handPoint;
    [SerializeField] private float _heightHand;

    private InputSystemReader _inputSystemReader;
    private SpriteRenderer _spriteRenderer;

    private bool _canTouch;
    private bool _isAttack;
    private float _lookDirection;
    private float _currentAttackDelay;

    private RaycastHit2D _hitTarget;

    public float AttackDelay => _attackDelay;
    public bool IsAttack => _isAttack;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputSystemReader = GetComponent<InputSystemReader>();
    }

    private void OnEnable()
    {
        _inputSystemReader.AttackButtonUsed += value => SetButtonValue(value);
    }

    private void Update()
    {
        CheckDistance();
        TryAttack(_attackDelay);
    }

    public void SetButtonValue(float value)
    {
        int attackValue = 1;

        if (value == attackValue)
        {
            _isAttack = true;
        }
    }

    private void CheckDistance()
    {
        if (_spriteRenderer.flipX == true)
            _lookDirection = 1;
        else if (_spriteRenderer.flipX == false)
            _lookDirection = -1;

        _hitTarget = Physics2D.Raycast(_handPoint.transform.position, _handPoint.transform.right * _lookDirection, _maxDistanceToAttack, _layerMask);

        if (_hitTarget)
            _canTouch = true;
        else
            _canTouch = false;
    }

    private void TryAttack(float delay)
    {
        if (_currentAttackDelay >= 0f && _currentAttackDelay <= delay)
        {
            _isAttack = false;
            _currentAttackDelay += Time.deltaTime;
        }
        else if (_canTouch && _isAttack)
        {
            _hitTarget.collider.TryGetComponent(out Enemy enemy);
            if (enemy != null)
                enemy.ApplyDamage(_damage);

            _currentAttackDelay = 0f;
        }
        else if (_currentAttackDelay >= delay && _isAttack)
        {
            _currentAttackDelay = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (_canTouch)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        if (_lookDirection == 1)
        {
            Gizmos.DrawLine(_handPoint.position, new Vector3(_handPoint.position.x + _maxDistanceToAttack, _handPoint.position.y));
        }
        else if (_lookDirection == -1)
        {
            Gizmos.DrawLine(_handPoint.position, new Vector3(_handPoint.position.x - _maxDistanceToAttack, _handPoint.position.y));
        }
    }
}
