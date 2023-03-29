using System;
using UnityEngine;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	[RequireComponent(typeof(UIElementGetterFacadeView))]
	public class MenuButtonViewModel : MonoBehaviour
	{
		private const string MenuButton = "MenuButton";

		[SerializeField] private GameObject _menuWindow;

		private UIElementGetterFacadeView _uiElementGetterFacadeView;
		private Button _button;

		private void Awake() => 
			_uiElementGetterFacadeView = GetComponent<UIElementGetterFacadeView>();

		private void OnEnable()
		{
			_button = _uiElementGetterFacadeView.GetUIElementQ<Button>(MenuButton);
			_button.clicked += OnButtonClicked;
		}

		private void OnDestroy()
		{
			_button.clicked -= OnButtonClicked;
		}

		private void OnButtonClicked()
		{
			gameObject.SetActive(false);
			_menuWindow.gameObject.SetActive(true);
		}
	}
}