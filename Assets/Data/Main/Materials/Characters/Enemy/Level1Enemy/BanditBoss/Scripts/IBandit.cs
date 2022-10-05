using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBandit
{
    int Health { get; }
    int Damage { get; }

    public void ApplyDamage(int damage);
}
