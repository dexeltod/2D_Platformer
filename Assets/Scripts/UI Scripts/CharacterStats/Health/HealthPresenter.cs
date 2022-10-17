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
        float a =  _slider.maxValue / _playerCharacter.MaxHealth;
        float b =  _slider.maxValue / _playerCharacter.CurrentHealth;

        _slider.value = a / b;
    }
}