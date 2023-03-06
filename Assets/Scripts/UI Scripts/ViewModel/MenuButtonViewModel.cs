using System;
using Infrastructure.GameLoading;
using Infrastructure.States;
using UnityEngine;

namespace UI_Scripts.ViewModel
{
	[RequireComponent(typeof(MenuButtonView))]
	public class MenuButtonViewModel : MonoBehaviour
	{
		private IGameStateMachine _gameStateMachine;
		[SerializeField] private MenuButtonView _buttonView;

		private void Awake()
		{
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		private void OnEnable()
		{
			_buttonView.PlayButtonPressed += PlayButtonPressed;
		}

		private void OnDisable()
		{
			_buttonView.PlayButtonPressed -= PlayButtonPressed;
		}

		private void PlayButtonPressed()
		{
			_gameStateMachine.Enter<SceneLoadState, string>("Level_1");
		}
	}
}