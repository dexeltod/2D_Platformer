using UnityEngine.UIElements;

namespace ViewModel.StartMenu.UIBuilder
{
	public class SliderUIProvider
	{
		private readonly UIElementGetterFacade _uiElementGetterFacade;

		public SliderUIProvider(UIElementGetterFacade uiElementGetterFacade) =>
			_uiElementGetterFacade = uiElementGetterFacade;

		public Slider GetSlider(string sliderType) =>
			_uiElementGetterFacade.GetFirst<Slider>(sliderType);

		public void SetSliderValue(string sliderType, float value)
		{
			var slider = _uiElementGetterFacade.GetFirst<Slider>(sliderType);
			slider.value = value;
		}
	}
}