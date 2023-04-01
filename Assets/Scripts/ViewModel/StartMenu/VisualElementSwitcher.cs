using UnityEngine.UIElements;

namespace ViewModel.MainMenu.Buttons
{
	public class VisualElementSwitcher
	{
		public void Enter(VisualElement from, VisualElement to)
		{
			IStyle lastElement = from.style;
			lastElement.display = DisplayStyle.None;
			
			IStyle openedElement = to.style;
			openedElement.display = DisplayStyle.Flex;
		}
	}
}