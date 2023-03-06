using UnityEngine;

namespace Game.CharactersSettingsSO.Characters.Enemy
{
    [CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Data/Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
	    [SerializeField] private float _walkSpeed;
	    [SerializeField] private float _attackSpeed;
	    [SerializeField] private float _runSpeed;
	    [SerializeField] private int _damage;
	    [SerializeField] private int _armor;
	    [SerializeField] private int _health;
	    [SerializeField] private int _criticalDamageChance;
	    [SerializeField] private float _idleTime;
    
        public int Damage => _damage;
        public int Health => _health;
        public float WalkSpeed => _walkSpeed;
        public float AttackSpeed => _attackSpeed;
        public float RunSpeed => _runSpeed;
        public float IdleTime => _idleTime;

        public int CriticalDamage => Damage * 2;
    }
}