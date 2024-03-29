using UnityEngine;

namespace Game.CharactersSettingsSO.Characters.Enemy
{
    [CreateAssetMenu(fileName = "newEntityData", menuName = "Enemy/DataEnemyVisibility")]
    public class DataEnemyVisibility : ScriptableObject
    {
	    [Header("Detect ground")]
        public float WallCheckDistance = 0.2f;
        public float LedgeCheckDistance = 0.4f;
        public LayerMask GroundLayer;
    }
}


