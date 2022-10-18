using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    public event UnityAction HealthChanged;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Transform _eyePosition;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public Transform EyePosition => _eyePosition;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        const int MinHealthValue = 0;
        _currentHealth -= Mathf.Clamp(damage, MinHealthValue, _maxHealth);
        HealthChanged?.Invoke();
    }

    public void Heal(int healCount)
    {
        const int MinHealthValue = 0;
        _currentHealth += Mathf.Clamp(healCount, MinHealthValue, _maxHealth);
        HealthChanged?.Invoke();
    }
}