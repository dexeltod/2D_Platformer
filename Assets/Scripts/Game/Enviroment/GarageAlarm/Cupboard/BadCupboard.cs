using Game.Enviroment.GarageAlarm.Cupboard.Based;
using UnityEngine;

namespace Game.Enviroment.GarageAlarm.Cupboard
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BadCupboard : BaseCupboard
    {
        [SerializeField] private AlarmIncreaser _alarm;
    }
}
