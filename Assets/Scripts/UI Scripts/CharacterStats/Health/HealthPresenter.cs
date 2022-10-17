using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _playerCharacter.HealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        _playerCharacter.HealthChanged -= SetHealth;
    }

    private void SetHealth()
    {
        float maxHealthNormalized =  _slider.maxValue / _playerCharacter.MaxHealth;
        float currentHealthNormalized =  _slider.maxValue / _playerCharacter.CurrentHealth;

        _slider.value = maxHealthNormalized / currentHealthNormalized;
    }
}