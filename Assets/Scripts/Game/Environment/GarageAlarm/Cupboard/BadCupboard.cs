using Game.Environment.GarageAlarm.Cupboard.Based;
using UnityEngine;

namespace Game.Environment.GarageAlarm.Cupboard
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BadCupboard : BaseCupboard
    {
        [SerializeField] private AlarmIncreaser _alarm;
    }
}
