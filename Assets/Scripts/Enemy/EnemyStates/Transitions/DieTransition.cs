using UnityEngine;

public class DieTransition : Transition
{
	[SerializeField] private Enemy _enemy;

	public override void Enable()
		=> _enemy.Dying += ChangeState;

	public void OnDisable() =>
		_enemy.Dying -= ChangeState;

	private void ChangeState(Enemy enemy)
	{
		enemy.Dying -= ChangeState;
		IsNeedTransition = true;
	}
}