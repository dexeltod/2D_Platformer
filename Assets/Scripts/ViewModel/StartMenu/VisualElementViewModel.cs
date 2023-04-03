using UnityEngine.UIElements;

namespace ViewModel.StartMenu
{
	public class VisualElementViewModel
	{
		public void Enter(VisualElement from, VisualElement to)
		{
			IStyle lastElement = from.style;
			lastElement.display = DisplayStyle.None;
			
			IStyle openedElement = to.style;
			openedElement.display = DisplayStyle.Flex;
		}

		public void Disable(VisualElement disabledElement)
		{
			IStyle lastElement = disabledElement.style;
			lastElement.display = DisplayStyle.None;
		}
	}
}