using Game.Environment.GarageAlarm.Cupboard.Interfaces;
using UnityEngine.Events;

namespace Game.Environment.GarageAlarm.Cupboard.Behaviours
{
    public class GoodCuboardBehavior : ICupboard
    {
        public UnityAction IsCupboardOpened;

        public void Open()
        {
            IsCupboardOpened?.Invoke();
        }
    }
}
