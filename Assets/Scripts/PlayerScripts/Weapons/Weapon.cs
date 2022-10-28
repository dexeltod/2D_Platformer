using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Damage { get; protected set; }
    public float AttackSpeed { get; protected set; }

    public abstract void Attack(float direction);
    public abstract void GiveDamage(Enemy target);
}