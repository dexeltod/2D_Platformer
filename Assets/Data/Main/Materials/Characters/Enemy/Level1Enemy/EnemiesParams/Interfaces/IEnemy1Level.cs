using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy1Level
{
    public int Health { get; }
    public int Damage { get; }

    public void Attack();
    public void ApplyDamage(int damage);
}