using UnityEngine;

public abstract class EnemyStateMachine
{
	protected readonly EnemyBehaviour EnemyBehaviour;
	protected readonly Animator Animator;
	protected readonly AnimationHasher AnimationHasher;
	protected readonly IEnemyStateSwitcher StateSwitcher;
	protected readonly EnemyObserver EnemyObserver;

	protected EnemyStateMachine(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator,
		AnimationHasher animationHasher, EnemyObserver enemyObserver)
	{
		EnemyObserver = enemyObserver;
		EnemyBehaviour = enemyBehaviour;
		StateSwitcher = stateSwitcher;
		Animator = animator;
		AnimationHasher = animationHasher;
	}
	
	public abstract void Start();
	public abstract void Stop();
}