// using UnityEngine;
//
// [RequireComponent(typeof(SpriteRenderer))]
// [RequireComponent(typeof(InputSystemReader))]
// [RequireComponent(typeof(PlayerAttackSettings))]
// [RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(AnimationHasher))]
//
// public class AnimationSwitcher : MonoBehaviour
// {
//     private Animator _animator;
//     private PlayerAttackSettings _playerAttack;
//     private InputSystemReader _inputSystemReader;
//     private SpriteRenderer _spriteRenderer;
//     private AnimationHasher _animationHasher;
//
//     private float _buttonMoveValue;
//
//     private void Awake()
//     {
//         _animator = GetComponent<Animator>();
//         _animationHasher = GetComponent<AnimationHasher>();
//         _spriteRenderer = GetComponent<SpriteRenderer>();
//         _inputSystemReader = GetComponent<InputSystemReader>();
//         _playerAttack = GetComponent<PlayerAttackSettings>();
//     }
//
//     private void OnEnable()
//     {
//         _playerAttack.IsAttacks += PlayAttack;
//         _inputSystemReader.VerticalMoveButtonUsed += PlayRun;
//         _inputSystemReader.VerticalMoveButtonUsed += PlayIdle;
//     }
//
//     private void OnDisable()
//     {
//         _playerAttack.IsAttacks -= PlayAttack;
//         _inputSystemReader.VerticalMoveButtonUsed -= PlayRun;
//         _inputSystemReader.VerticalMoveButtonUsed -= PlayIdle;
//     }
//
//     private void PlayAttack()
//     {
//         _animator.Play(Animator.StringToHash("isAttack"));
//     }
//
//     private void PlayRun(float direction)
//     {
//         _animator.Play(_animationHasher.RacingHash);
//         _animator.Play(_animationHasher.RunHash);
//
//         _spriteRenderer.flipX = _buttonMoveValue < 0;
//     }
//
//     private void PlayIdle(float direction)
//     {
//         if (direction == 0)
//             _animator.Play(_animationHasher.IdleHash);
//     }
// }