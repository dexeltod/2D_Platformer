using UnityEngine;

namespace Game.Enemy.EnemySettings.TestEnemy.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Data/Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public int Damage = 10;
        public int Armor = 0;
        public int Health = 50;
        public float WalkSpeed = 1;
        public int CriticalDamageChance = 0;
        public float AttackSpeed = 2;
        public float AttackRange = 2;
        public float RunSpeed = 2;
        public float IdleTime = 3;

        public int CriticalDamage => Damage * 2;
    }
}