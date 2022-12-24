using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Slider _slider;

    private Coroutine _currentCoroutine;

    private void OnEnable() => 
	    _player.HealthChanged += Set;

    private void OnDisable() => 
	    _player.HealthChanged -= Set;

    private void Set()
    {
        float maxHealthNormalized = _slider.maxValue / _player.MaxHealth;
        float currentHealthNormalized = _slider.maxValue / _player.CurrentHealth;
        float neededValue = maxHealthNormalized / currentHealthNormalized;

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        
        _currentCoroutine = StartCoroutine(SetValueSmooth(neededValue));
    }
    
    private IEnumerator SetValueSmooth(float neededValue)
    {
        var waitingFixedUpdate = new WaitForFixedUpdate();
        float smoothValue = 0.01f;

        while (_slider.value != neededValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, neededValue, smoothValue);
            yield return waitingFixedUpdate;
        }
    }
}