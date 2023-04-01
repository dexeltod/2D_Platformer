using UnityEngine.UIElements;

namespace View.StartMenu.UIBuilder.SliderViewModel
{
	public class SliderUIProvider
	{
		private readonly UIElementGetterFacade _uiElementGetterFacade;

		public SliderUIProvider(UIElementGetterFacade uiElementGetterFacade) =>
			_uiElementGetterFacade = uiElementGetterFacade;

		public Slider GetSlider(string sliderType) =>
			_uiElementGetterFacade.GetUIElementQ<Slider>(sliderType);

		public void SetSliderValue(string sliderType, float value)
		{
			var slider = _uiElementGetterFacade.GetUIElementQ<Slider>(sliderType);
			slider.value = value;
		}
	}
}