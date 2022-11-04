using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Enemy CreateEnemy(Transform point, Enemy enemy, PlayerHealth playerHealth)
    {
        var currentEnemy = Instantiate(enemy, point);
        currentEnemy.Initialize(playerHealth);
        return currentEnemy;
    }
}