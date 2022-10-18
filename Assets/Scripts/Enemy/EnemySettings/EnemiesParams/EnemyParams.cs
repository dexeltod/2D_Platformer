using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyObject", order = 1)]
public class EnemyParams : ScriptableObject
{
    public string enemyName;
    public float life;
    public int damage;
}