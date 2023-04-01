using System.Collections.Generic;
using System.Xml.Serialization;
using Infrastructure.GameLoading;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	public class LevelsMenu : MenuElement
	{
		private const string LevelsRow = "LevelsRow";
		private readonly VisualElement _menu;
		private readonly Button _menuButton;
		private readonly List<Button> _levels = new();
		private readonly IGameStateMachine _gameStateMachine;

		public LevelsMenu(VisualElement thisElement, VisualElementSwitcher visualElementSwitcher,
			UIElementGetterFacade elementGetter) : base(thisElement, visualElementSwitcher, elementGetter)
		{
			_menu = ElementGetter.GetUIElementQ<VisualElement>(MenuVisualElementNames.Menu);
			_menuButton = ElementGetter.GetUIElementQ<Button>(UiButtonNames.Menu);
			_menuButton.clicked += OnMenuButtonPressed;
			_levels = ElementGetter.GetUIElementChildren<Button>(LevelsRow);
			RegisterOnLevels();
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		~LevelsMenu()
		{
			_menuButton.clicked -= OnMenuButtonPressed;
			UnregisterFromLevels();
		}

		private void UnregisterFromLevels()
		{
			foreach (var level in _levels)
				level.UnregisterCallback<ClickEvent>(OnLevelSelected);
		}

		private void OnMenuButtonPressed() =>
			VisualElementSwitcher.Enter(ThisElement, _menu);

		private void RegisterOnLevels()
		{
			foreach (var level in _levels)
				level.RegisterCallback<ClickEvent>(OnLevelSelected);
		}

		private void OnLevelSelected(ClickEvent clicked)
		{
			VisualElement level = (VisualElement)clicked.target;
			string levelName = level.name;
			Debug.Log(levelName);
		}
	}
}