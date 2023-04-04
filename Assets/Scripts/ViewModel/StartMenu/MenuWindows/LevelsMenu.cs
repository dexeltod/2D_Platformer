using System.Collections.Generic;
using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.Services.Interfaces;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UIElements;
using ViewModel.StartMenu.UIBuilder;

namespace ViewModel.StartMenu.MenuWindows
{
	public class LevelsMenu : MenuElement
	{
		private const string LevelsRow = "LevelsRow";
		private readonly VisualElement _menu;
		private readonly Button _menuButton;
		private readonly List<Button> _levels;
		private readonly IGameStateMachine _gameStateMachine;
		private readonly ISceneConfigGetter _sceneConfigGetter;

		public LevelsMenu(VisualElement thisElement, VisualElementViewModel visualElementSwitcher,
			UIElementGetterFacade elementGetter, ISceneConfigGetter sceneConfigGetter,
			IGameStateMachine gameStateMachine) : base(thisElement,
			visualElementSwitcher, elementGetter)
		{
			_menu = ElementGetter.GetFirst<VisualElement>(MenuVisualElementNames.Menu);
			_menuButton = ElementGetter.GetFirstInChildren<Button>(ThisElement, UiButtonNames.Menu);
			_menuButton.clicked += OnMenuButtonPressed;
			_levels = ElementGetter.GetChildren<Button>(LevelsRow);
			Register();
			_gameStateMachine = gameStateMachine;
			_sceneConfigGetter = sceneConfigGetter;
		}

		~LevelsMenu()
		{
			UnregisterFromLevels();
		}

		private void UnregisterFromLevels()
		{
			_menuButton.clicked -= OnMenuButtonPressed;

			foreach (var level in _levels)
				level.UnregisterCallback<ClickEvent>(OnLevelSelected);
		}

		private void Register()
		{
			foreach (var level in _levels)
				level.RegisterCallback<ClickEvent>(OnLevelSelected);
		}

		private void OnMenuButtonPressed() =>
			VisualElementController.Enter(ThisElement, _menu);

		private async void OnLevelSelected(ClickEvent clicked)
		{
			VisualElementController.Disable(ThisElement);
			VisualElement level = (VisualElement)clicked.target;
			string levelName = level.name;

			SceneConfig sceneConfig = await _sceneConfigGetter.GetSceneConfig(levelName);
			_gameStateMachine.Enter<SceneLoadState, string, bool>(sceneConfig.SceneName,
				sceneConfig.MusicName, sceneConfig.IsStopMusicBetweenScenes);
		}
	}
}