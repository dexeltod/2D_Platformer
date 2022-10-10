public class BadCupboardBehaviour : ICupboard
{
    private readonly AlarmIncreaser _alarmIncreaser;

    public BadCupboardBehaviour(AlarmIncreaser alarmIncreaser)
    {
        _alarmIncreaser = alarmIncreaser;
    }

    public void Open()
    {
        _alarmIncreaser.SetMaxAlarmVolume();
    }
}
