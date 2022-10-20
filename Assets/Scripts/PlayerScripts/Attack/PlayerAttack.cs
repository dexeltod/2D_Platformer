using UnityEngine;

[RequireComponent(typeof(InputSystemReader))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAttack : MonoBehaviour
{
    [Header("Set target and damage")] [SerializeField]
    private int _damage = 10;
    
    [SerializeField] private float _attackDelay;

    [Header("Set hand point")] [SerializeField]
    private Transform _handPoint;

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
        _inputSystemReader.AttackButtonUsed += SetButtonValue;
    }

    private void Update()
    {
        CheckDistance();
        TryAttack(_attackDelay);
    }

    private void SetButtonValue(float value)
    {
        float attackValue = 1f;

        if (value == attackValue)
            _isAttack = true;
    }

    private void CheckDistance()
    {
        _lookDirection = _spriteRenderer.flipX ? -1 : 1;
        // _hitTarget = Vector2.Distance(transform.position, _);
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
            _currentAttackDelay = 0f;
    }
}