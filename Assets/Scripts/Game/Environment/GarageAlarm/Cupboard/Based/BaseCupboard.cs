using Game.Environment.GarageAlarm.Cupboard.Interfaces;
using UnityEngine;

namespace Game.Environment.GarageAlarm.Cupboard.Based
{
    public abstract class BaseCupboard : MonoBehaviour
    {
        protected ICupboard Cupboard;

        public void SetCupboard(ICupboard cupboard)
        {
            Cupboard = cupboard;
        }

        protected void Open()
        {
            Cupboard.Open();
        }
    }
}
