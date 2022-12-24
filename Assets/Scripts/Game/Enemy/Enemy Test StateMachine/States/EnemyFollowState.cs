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
		_enemyFollow.PlayerAbove += OnSetIdle;
		EnemyObserver.SeenPlayer += SetIdleIfSeePlayer;
		EnemyObserver.TouchedPlayer += TrySetAttackState;
		Animator.Play(AnimationHasher.RunHash);
	}

	private void OnSetIdle() => 
		EnemyBehaviour.SetIdleState();

	private void SetIdleIfSeePlayer(bool canSeePlayer)
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