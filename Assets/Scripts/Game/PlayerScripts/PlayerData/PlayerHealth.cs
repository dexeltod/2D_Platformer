using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerScripts.PlayerData
{
    public class PlayerHealth : MonoBehaviour
    {
        public event UnityAction Died;
        public event UnityAction HealthChanged;

        [SerializeField] private int _maxHealth = 100;

        
        public int CurrentHealth { get; private set; }
        public int MaxHealth => _maxHealth;

        private void Start()
        {
	        CurrentHealth = _maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            int minHealthValue = 0;

            if (CurrentHealth <= minHealthValue)
            {
                Died?.Invoke();
                return;
            }

            CurrentHealth -= damage;
            ValidateHealth();

            HealthChanged?.Invoke();
        }

        public void Heal(int healCount)
        {
	        CurrentHealth += healCount;
            ValidateHealth();
            HealthChanged?.Invoke();
        }

        private void ValidateHealth()
        {
            const int MinHealthValue = 0;
            CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealthValue, _maxHealth);
        }
    }
}