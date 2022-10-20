using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Data/Enemy/EnemyData")]
public class DataEnemy : ScriptableObject
{
    public int Damage = 10;
    public int Armor= 0 ;
    public int Health = 50;
    public float MoveSpeed = 1;
    public int CriticalDamageChance = 0;
    public float AttackDelay= 2;
    public float AttackRange = 2;
    
    public int CriticalDamage => Damage * 2;
}