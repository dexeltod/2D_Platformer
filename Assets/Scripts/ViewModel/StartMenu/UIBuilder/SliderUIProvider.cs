using UnityEngine.UIElements;

namespace View.StartMenu.UIBuilder.SliderViewModel
{
	public class SliderUIProvider
	{
		private readonly UIElementGetterFacadeView _uiElementGetterFacadeView;

		public SliderUIProvider(UIElementGetterFacadeView uiElementGetterFacadeView) =>
			_uiElementGetterFacadeView = uiElementGetterFacadeView;

		public Slider GetSlider(string sliderType) =>
			_uiElementGetterFacadeView.GetUIElementQ<Slider>(sliderType);

		public void SetSliderValue(string sliderType, float value)
		{
			var slider = _uiElementGetterFacadeView.GetUIElementQ<Slider>(sliderType);
			slider.value = value;
		}
	}
}