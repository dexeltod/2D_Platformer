using System;
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
        
        if(_currentHealth <= MinHealthValue)
            return;
        
        _currentHealth -= damage;
        HealthChanged?.Invoke();
    }

    public void Heal(int healCount)
    {
        if(_currentHealth >= _maxHealth)
            return;
        
        _currentHealth += healCount;
        HealthChanged?.Invoke();
    }
}