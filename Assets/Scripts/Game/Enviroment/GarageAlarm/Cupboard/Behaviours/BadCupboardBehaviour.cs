using Game.Enviroment.GarageAlarm.Cupboard.Interfaces;

namespace Game.Enviroment.GarageAlarm.Cupboard.Behaviours
{
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
}
