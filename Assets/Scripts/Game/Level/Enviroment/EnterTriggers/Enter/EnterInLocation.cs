using Infrastructure;
using Infrastructure.Services;
using UnityEngine;

public class EnterInLocation : MonoBehaviour
{
	[SerializeField] private Transform _placeTransform;
	
	private Transform _playerTransform;
	private IInputService _inputService;
	private IGameFactory _factory;

	private void Awake()
	{
		_inputService = ServiceLocator.Container.Single<IInputService>();
		_factory = ServiceLocator.Container.Single<IGameFactory>();
		_factory.MainCharacterCreated += OnLevelLoaded;
	}

	private void OnLevelLoaded()
	{
		_playerTransform = _factory.MainCharacter.transform;
		_factory.MainCharacterCreated -= OnLevelLoaded;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out PlayerHealth player))
		{
			EnableButton();
		}
	}

	private void OnTriggerExit2D()
	{
		DisableButton();
	}

	private void EnableButton()
	{
		_inputService.InteractButtonUsed += Enter;
	}

	private void DisableButton()
	{
		_inputService.InteractButtonUsed -= Enter;
	}

	private void Enter()
	{
		DisableButton();
		_playerTransform.position = _placeTransform.position;
	}
}