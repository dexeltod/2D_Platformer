using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	public abstract class MenuElement : IVisualElement
	{
		public VisualElement ThisElement { get; }

		protected readonly VisualElementSwitcher VisualElementSwitcher;
		protected readonly UIElementGetterFacade ElementGetter;

		protected MenuElement(VisualElement thisElement, VisualElementSwitcher visualElementSwitcher, UIElementGetterFacade elementGetter)
		{
			ThisElement = thisElement;
			VisualElementSwitcher = visualElementSwitcher;
			ElementGetter = elementGetter;
		}
	}
}