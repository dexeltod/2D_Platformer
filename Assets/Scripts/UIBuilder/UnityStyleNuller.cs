using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;
using ViewModel.MainMenu.Buttons;

namespace UIBuilder
{
	[RequireComponent(typeof(UIElementGetterFacadeView))]
	[RequireComponent(typeof(UIDocument))]
	public class UnityStyleNuller : MonoBehaviour
	{
		[SerializeField] private bool _isNullSlidersStyle = true;

		private UIElementGetterFacadeView _getterFacade;
		private VisualElement _root;
		private List<Slider> _sliders;

		private void Awake()
		{
			_getterFacade = GetComponent<UIElementGetterFacadeView>();
			NullAllSlidersStyle();
		}

		private void NullAllSlidersStyle()
		{
			if (_isNullSlidersStyle == false)
				return;
			
			_sliders = new()
			{
				_getterFacade.GetUIElementQ<Slider>(UIButtonsNames.Master),
				_getterFacade.GetUIElementQ<Slider>(UIButtonsNames.Music),
			};

			foreach (var slider in _sliders) 
				slider.focusable = false;
		}
	}
}