using System.Collections.Generic;
using UnityEngine;
using ViewModel.StartMenu.MenuWindows;
using ViewModel.StartMenu.UIBuilder;

namespace View.StartMenu.SettingsWindow.Sliders
{
	public class SettingsSliderView 
	{
		private readonly SettingsMenu _settingsMenu;
		private readonly SliderUIProvider _sliderUIProvider;

		public SettingsSliderView(UIElementGetterFacade elementGetterFacade, SettingsMenu settingsMenu)
		{
			_settingsMenu = settingsMenu;
			_sliderUIProvider = new SliderUIProvider(elementGetterFacade);
			_settingsMenu.SettingsChanged += OnSettingsChanged;
		}

		~SettingsSliderView()
		{
			_settingsMenu.SettingsChanged -= OnSettingsChanged;
		}

		private void OnSettingsChanged(Dictionary<string, float> sliderValues)
		{
			foreach (var value in sliderValues)
				_sliderUIProvider.SetSliderValue(value.Key, value.Value);
		}
	}
}