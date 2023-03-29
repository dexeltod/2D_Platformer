using System;
using System.Collections.Generic;
using UnityEngine;
using View.StartMenu.UIBuilder;
using View.StartMenu.UIBuilder.SliderViewModel;
using ViewModel.MainMenu.Buttons;

namespace View.StartMenu.SettingsWindow.Sliders
{
	public class SettingsSliderView : MonoBehaviour
	{
		[SerializeField] private SaveSettingsButtonViewModel _saveSettingsButtonViewModel;
		[SerializeField] private UIElementGetterFacadeView _elementGetterFacadeView;
		
		private SliderUIProvider _sliderUIProvider;

		private void Awake()
		{
			_sliderUIProvider = new(_elementGetterFacadeView);
			_saveSettingsButtonViewModel.SettingsChanged += OnSettingsChanged;
		}

		~SettingsSliderView()
		{
			_saveSettingsButtonViewModel.SettingsChanged -= OnSettingsChanged;
		}

		private void OnSettingsChanged(Dictionary<string, float> sliderValues)
		{
			foreach (var value in sliderValues) 
				_sliderUIProvider.SetSliderValue(value.Key, value.Value);
		}
	}
}