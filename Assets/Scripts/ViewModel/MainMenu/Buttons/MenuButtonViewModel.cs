using System;
using UnityEngine;
using UnityEngine.UIElements;
using View.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	[RequireComponent(typeof(UIElementGetterView))]
	public class MenuButtonViewModel : MonoBehaviour
	{
		private const string MenuButton = "MenuButton";

		[SerializeField] private GameObject _menuWindow;

		private UIElementGetterView _uiElementGetterView;
		private Button _button;

		private void Awake() => 
			_uiElementGetterView = GetComponent<UIElementGetterView>();

		private void OnEnable()
		{
			_button = _uiElementGetterView.GetUIElement<Button>(MenuButton);
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