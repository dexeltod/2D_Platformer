using UnityEngine;

public class EnemyPatrolState : EnemyStateMachine
{
	private readonly EnemyPatrolBehaviour _enemyPatrolBehaviour;

	public EnemyPatrolState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator,
		AnimationHasher animationHasher, EnemyObserver enemyObserver, EnemyPatrolBehaviour enemyPatrolBehaviour) : base(
		enemyBehaviour, stateSwitcher, animator, animationHasher, enemyObserver)
	{
		_enemyPatrolBehaviour = enemyPatrolBehaviour;
	}

	public override void Start()
	{
		Animator.Play(AnimationHasher.WalkHash);
		_enemyPatrolBehaviour.enabled = true;
		_enemyPatrolBehaviour.NoWay += SetIdleState;
		EnemyObserver.TouchedPlayer += SetAttackState;
		EnemyObserver.SeenPlayer += SetFollowState;
	}

	private void SetIdleState() => 
		StateSwitcher.SwitchState<EnemyIdleState>();

	private void SetAttackState(bool canSeePlayer)
	{
		if (canSeePlayer == true) 
			StateSwitcher.SwitchState<EnemyAttackState>();
	}

	private void SetFollowState(bool canSeePlayer)
	{
		if (canSeePlayer == true) 
			StateSwitcher.SwitchState<EnemyFollowState>();
	}

	public override void Stop()
	{
		_enemyPatrolBehaviour.NoWay -= SetIdleState;
		EnemyObserver.TouchedPlayer -= SetAttackState;
		EnemyObserver.SeenPlayer -= SetFollowState;
		_enemyPatrolBehaviour.enabled = false;
	}
}