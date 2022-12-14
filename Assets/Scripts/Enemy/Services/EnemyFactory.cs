using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public EnemyAttackBehaviour CreateEnemy(Transform point, EnemyAttackBehaviour enemy, PlayerHealth playerHealth)
    {
        var currentEnemy = Instantiate(enemy, point);
        currentEnemy.Initialize(playerHealth);
        return currentEnemy;
    }
}