using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Data/EnemyEntity/AttackData")]
public class DataEnemyFight : ScriptableObject
{
    public int Damage;
    public int Armor;
    
    public int CriticalDamage => Damage * 2;
    public float AttackSpeed;

}
