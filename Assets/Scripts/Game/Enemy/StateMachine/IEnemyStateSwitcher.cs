namespace Game.Enemy.StateMachine
{
    public interface IEnemyStateSwitcher
    {
        public void SwitchState<T>() where T : EnemyStateMachine;
    }
}