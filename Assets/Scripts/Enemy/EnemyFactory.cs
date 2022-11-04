using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTypes;

    public Enemy CreateEnemy(Transform point, Enemy enemy)
    {
        return Instantiate(enemy, point);
    }
}