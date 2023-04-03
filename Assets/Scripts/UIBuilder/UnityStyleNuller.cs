using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using ViewModel.StartMenu;
using ViewModel.StartMenu.UIBuilder;

namespace UIBuilder
{
	[RequireComponent(typeof(UIElementGetterFacade))]
	public class UnityStyleNuller : MonoBehaviour
	{
		[SerializeField] private bool _isNullSlidersStyle = true;

		private UIElementGetterFacade _getterFacade;
		private VisualElement _root;
		private List<Slider> _sliders;

		private void Awake()
		{
			_getterFacade = GetComponent<UIElementGetterFacade>();
			NullAllSlidersStyle();
		}

		private void NullAllSlidersStyle()
		{
			if (_isNullSlidersStyle == false)
				return;
			
			_sliders = new()
			{
				_getterFacade.GetFirst<Slider>(UiSliderNames.Master),
				_getterFacade.GetFirst<Slider>(UiSliderNames.Music),
			};

			foreach (var slider in _sliders) 
				slider.focusable = false;
		}
	}
}