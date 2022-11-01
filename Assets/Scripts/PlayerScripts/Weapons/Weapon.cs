using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Damage { get; protected set; }
    public float AttackSpeed { get; protected set; }

    public abstract IEnumerator AttackRoutine(float direction);
    public abstract void GiveDamage(Enemy target);
}