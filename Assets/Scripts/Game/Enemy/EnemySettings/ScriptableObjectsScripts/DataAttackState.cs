using UnityEngine;

namespace Game.Enemy.EnemySettings.ScriptableObjectsScripts
{
    [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/State Data Data/Attack State")]
    public class DataAttackState : ScriptableObject
    {
        public int Damage = 3;
        public float AttackRange = 2f;
        public float AttackDelay = 2f;

        public LayerMask WhatIsAttack;
    }
}
