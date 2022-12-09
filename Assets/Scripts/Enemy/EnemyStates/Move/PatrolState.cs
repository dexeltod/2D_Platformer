using UnityEngine;

public class PatrolState : State
{
	[SerializeField] private EnemyPatrolBehaviour _patrolBehaviour;

	private const string AnimBoolName = "isWalk";

	private Animator _animator;

	private void Awake() =>
		_animator = GetComponentInParent<Animator>();

	private void OnEnable() =>
		SetMovementState(true);

	private void OnDisable() =>
		SetMovementState(false);

	private void SetMovementState(bool workState)
	{
		_patrolBehaviour.enabled = workState;
		_animator.SetBool(AnimBoolName, workState);
	}
}