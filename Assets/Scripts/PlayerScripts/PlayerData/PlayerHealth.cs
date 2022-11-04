using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public event UnityAction Died;
    public event UnityAction HealthChanged;

    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        int minHealthValue = 0;

        if (_currentHealth <= minHealthValue)
        {
            Died?.Invoke();
            return;
        }

        Debug.Log($"_currentHealth {_currentHealth}");
        _currentHealth -= damage;
        ValidateHealth();
        
        HealthChanged?.Invoke();
    }

    public void Heal(int healCount)
    {
        _currentHealth += healCount;
        ValidateHealth();
        HealthChanged?.Invoke();
    }

    private void ValidateHealth()
    {
        const int MinHealthValue = 0;
        _currentHealth = Mathf.Clamp(_currentHealth, MinHealthValue, _maxHealth);
    }
}