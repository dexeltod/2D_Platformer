using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Slider _slider;

    private Coroutine _currentCoroutine;

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
        float maxHealthNormalized = _slider.maxValue / _playerCharacter.MaxHealth;
        float currentHealthNormalized = _slider.maxValue / _playerCharacter.CurrentHealth;
        float neededValue = maxHealthNormalized / currentHealthNormalized;

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        
        _currentCoroutine = StartCoroutine(SetValueSmooth(neededValue));
    }
    
    private IEnumerator SetValueSmooth(float neededValue)
    {
        var waitingFixedUpdate = new WaitForFixedUpdate();
        float smoothValue = 0.03f;

        while (_slider.value != neededValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, neededValue, smoothValue);
            yield return waitingFixedUpdate;
        }
    }
}