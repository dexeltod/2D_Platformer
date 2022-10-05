using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/State Data Data/Attack State")]
public class D_AttackState : ScriptableObject
{
    public int Damage = 3;
    public float AttackRange = 2f;

    public LayerMask WhatIsAttack;
}
