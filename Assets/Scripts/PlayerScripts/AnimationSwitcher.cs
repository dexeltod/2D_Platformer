using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputSystemReader))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Animator))]

public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private PlayerAttack _playerAttack;
    private InputSystemReader _inputSystemReader;
    private SpriteRenderer _spriteRenderer;
    private CharacterAnimationHasher _animationHasher;

    private float _buttonMoveValue;

    private void Awake()
    {
        _animationHasher = gameObject.AddComponent<CharacterAnimationHasher>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputSystemReader = GetComponent<InputSystemReader>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnEnable()
    {
        _inputSystemReader.VerticalMoveButtonUsed += direction => _buttonMoveValue = direction;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SwitchAnimation();
    }

    private void OnDisable()
    {
        _inputSystemReader.VerticalMoveButtonUsed -= (direction) => direction = _buttonMoveValue;
    }

    private void SwitchAnimation()
    {
        if (_playerAttack.IsAttack)
        {
            _animator.SetBool(_animationHasher.AttackHash, true);
            _animator.SetFloat(_animationHasher.AttackSpeedHash, _playerAttack.AttackDelay);
        }
        else
            _animator.SetBool(_animationHasher.AttackHash, false);

        if (_buttonMoveValue > 0 || _buttonMoveValue < 0)
        {
            _animator.SetBool(_animationHasher.IdleHash, false);
            _animator.SetBool(_animationHasher.RacingHash, true);
            _animator.SetBool(_animationHasher.RunHash, true);
        }
        else if (_buttonMoveValue == 0)
        {
            _animator.SetBool(_animationHasher.IdleHash, false);
            _animator.SetBool(_animationHasher.RacingHash, false);
            _animator.SetBool(_animationHasher.RunHash, false);
        }

        if (_buttonMoveValue < 0)
            _spriteRenderer.flipX = true;
        else if (_buttonMoveValue > 0)
            _spriteRenderer.flipX = false;
    }
}
