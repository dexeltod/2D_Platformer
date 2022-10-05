using UnityEngine;

public class PlayerParams : MonoBehaviour
{
    private int _health;

    public void ApplyDamage(int damage)
    {
        _health -= damage;
    }

    private void Update()
    {
        HealthCheck();
    }

    private void HealthCheck()
    {
        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }
}
