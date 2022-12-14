public interface IEnemyStateSwitcher
{
	public void SwitchState<T>() where T : EnemyStateMachine;
}