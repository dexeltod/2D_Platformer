using System;
using System.Collections;
using System.Collections.Generic;
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
        float maxHealthNormalized = _slider.maxValue / _playerCharacter.MaxHealth;
        float currentHealthNormalized = _slider.maxValue / _playerCharacter.CurrentHealth;
        float neededValue = maxHealthNormalized / currentHealthNormalized;

        StartCoroutine(SetValueSmooth(neededValue));
    }

    private IEnumerator SetValueSmooth(float neededValue)
    {
        float smoothValue = 0.01f;
        
        while (Math.Abs(_slider.value - neededValue) > 0.01f)
        {
            _slider.value = Mathf.MoveTowards(_slider.value,neededValue, smoothValue);
            yield return new WaitForFixedUpdate();
        }
    }
}