using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyDieState : EnemyStateMachine
{
	private readonly CapsuleCollider2D _capsuleCollider2D;
	private readonly Rigidbody2D _rigidbody2D;
	private readonly ShadowCaster2D _shadowCaster2D;

	public EnemyDieState(
		EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator, AnimationHasher animationHasher,
		EnemyObserver enemyObserver, CapsuleCollider2D collider,
		Rigidbody2D rigidbody2D, ShadowCaster2D shadowCaster2D) 
		: base(enemyBehaviour,
		stateSwitcher, animator, animationHasher, enemyObserver)
	{
		_shadowCaster2D = shadowCaster2D;
		_capsuleCollider2D = collider;
		_rigidbody2D = rigidbody2D;
	}

	public override void Start()
	{
		Animator.Play(AnimationHasher.DieHash);
		EnemyObserver.enabled = false;
		EnemyBehaviour.enabled = false;
		_shadowCaster2D.enabled = false;
		_capsuleCollider2D.enabled = false;
		_rigidbody2D.bodyType = RigidbodyType2D.Static;
	}

	public override void Stop()
	{
	}
}