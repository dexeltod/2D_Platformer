
using UnityEngine;

namespace Game.Enemy.EnemySettings.ScriptableObjectsScripts
{
    [CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data Data/Idle state")]
    public class DataIdleState : ScriptableObject
    {
        public float MinIdleTime = 1f;
        public float MaxIdleTime = 3f;
    }
}
