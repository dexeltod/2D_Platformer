using System.Collections;
using Game.PlayerScripts.PlayerData;
using Infrastructure.GameLoading;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI_Scripts.CharacterStats.Health
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private PlayerHealth _playerHealth;

        private Coroutine _currentCoroutine;
        private IPlayerFactory _playerFactory;
        private IUIFactory _uiFactory;
        private ISceneLoadInformer _sceneLoadInformer;

        private void Awake()
        {
	        _sceneLoadInformer = ServiceLocator.Container.GetSingle<ISceneLoadInformer>();

	        _sceneLoadInformer.SceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded()
        {
	        _sceneLoadInformer.SceneLoaded -= OnSceneLoaded;
	        
	        _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
	        _playerHealth = _playerFactory.MainCharacter.GetComponent<PlayerHealth>();
	        _playerHealth.HealthChanged += OnSetHealth;
        }

        ~HealthPresenter()
        {
	        _playerHealth.HealthChanged -= OnSetHealth;
        }

        private void OnDisable()
        {
	        if(_playerHealth != null)
		        _playerHealth.HealthChanged -= OnSetHealth;
        }

        private void OnSetHealth()
        {
            float maxHealthNormalized = _slider.maxValue / _playerHealth.MaxHealth;
            float currentHealthNormalized = _slider.maxValue / _playerHealth.CurrentHealth;
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
}