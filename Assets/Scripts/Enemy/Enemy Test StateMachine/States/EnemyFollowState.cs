using UnityEngine;

public class EnemyFollowState : EnemyStateMachine
{
	private readonly EnemyFollowPlayerBehaviour _enemyFollow;
	
	public EnemyFollowState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator,
		AnimationHasher animationHasher, EnemyObserver enemyObserver, EnemyFollowPlayerBehaviour enemyFollow) : base(enemyBehaviour, stateSwitcher, animator, animationHasher, enemyObserver)
	{
		_enemyFollow = enemyFollow;
	}

	public override void Start()
	{
		_enemyFollow.enabled = true;
		EnemyObserver.SeenPlayer += SetIdleState;
		EnemyObserver.TouchedPlayer += TrySetAttackState;
		Animator.Play(AnimationHasher.RunHash);
	}

	private void SetIdleState(bool canSeePlayer)
	{
		if (canSeePlayer == false) 
			EnemyBehaviour.SetIdleState();
	}
	
	private void TrySetAttackState(bool isTouched)
	{
		if(isTouched)
			StateSwitcher.SwitchState<EnemyAttackState>();
	}
	
	public override void Stop()
	{
		_enemyFollow.enabled = false;
		EnemyObserver.TouchedPlayer -= TrySetAttackState;
	}
}