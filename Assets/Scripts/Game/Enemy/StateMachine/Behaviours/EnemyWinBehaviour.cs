using Game.Animation.AnimationHashes.Characters;
using UnityEngine;

namespace Game.Enemy.StateMachine.Behaviours
{
    [RequireComponent(typeof(AnimationHasher), typeof(Animator))]
    public class EnemyWinBehaviour : MonoBehaviour
    {
        private Animator _animator;
        private AnimationHasher _animation;

        private void Awake()
        {
            _animation = GetComponent<AnimationHasher>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() =>
            _animator.Play(_animation.FunHash);
    }
}