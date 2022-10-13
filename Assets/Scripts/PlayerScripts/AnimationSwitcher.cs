using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private PlayerAttack _playerAttack;
    private InputSystemReader _inputSystemReader;
    private SpriteRenderer _spriteRenderer;

    private float _buttonMoveValue;

    private void Awake()
    {
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
        Debug.Log(_buttonMoveValue);
        SwitchAnimation();
    }

    private void OnDisable()
    {
        _inputSystemReader.VerticalMoveButtonUsed -= (direction) => direction = _buttonMoveValue;
    }

    private void SwitchAnimation()
    {
        if(_playerAttack.IsAttack)
        {
            _animator.SetBool("isAttack", true);
            _animator.SetFloat("attackSpeed", _playerAttack.AttackDelay);
        }
        else
        {
            _animator.SetBool("isAttack", false);
        }

        if (_buttonMoveValue > 0 || _buttonMoveValue < 0)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isRacing", true);
            _animator.SetBool("isRunning", true);
        }
        else if(_buttonMoveValue == 0)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isRacing", false);
            _animator.SetBool("isRunning", false);
        }

        if (_buttonMoveValue < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_buttonMoveValue > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
