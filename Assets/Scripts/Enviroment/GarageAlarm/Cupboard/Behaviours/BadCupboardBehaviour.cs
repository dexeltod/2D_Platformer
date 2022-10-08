using UnityEngine;
using UnityEngine.Events;

public class BadCupboardBehaviour : ICupboard
{
    public UnityAction IsCupboardOpened;
    private AlarmIncreaser _alarmIncreaser;

    public BadCupboardBehaviour(AlarmIncreaser alarmIncreaser)
    {
        _alarmIncreaser = alarmIncreaser;
    }

    public void Open()
    {
        _alarmIncreaser.SetMaxAlarmVolume();
    }
}
