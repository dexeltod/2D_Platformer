using Game.Enviroment.GarageAlarm.Cupboard.Interfaces;
using UnityEngine.Events;

namespace Game.Enviroment.GarageAlarm.Cupboard.Behaviours
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
