namespace PlayerScripts
{
    public interface IPlayerStateSwitcher
    {
        void SwitchState<T>() where T : PlayerStateMachine;
    }
}