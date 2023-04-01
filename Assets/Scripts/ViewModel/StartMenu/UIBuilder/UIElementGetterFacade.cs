using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.StartMenu.UIBuilder
{
	public class UIElementGetterFacade : MonoBehaviour
	{
		[SerializeField] private UIDocument _uiDocument;

		private UQueryBuilder<VisualElement> _queryBuilder;

		public T GetUIElementQ<T>(string elementType) where T : VisualElement =>
			_uiDocument.rootVisualElement.Q<T>(elementType);

		public T GetUIElementQuery<T>(string elementType) where T : VisualElement =>
			_uiDocument.rootVisualElement.Query<T>(elementType);

		public List<T> GetUIElementChildren<T>(string elementType) where T : VisualElement
		{
			
			VisualElement parentElement = _uiDocument.rootVisualElement.Q<VisualElement>(elementType);
			IEnumerable<VisualElement> children = parentElement.Children();

			List<T> elements = new();

			foreach (var child in children)
				elements.Add(child.Query<T>());

			return elements;
		}

		public List<T> GetAllElementsByType<T>() where T: VisualElement
		{
			_queryBuilder = new(_uiDocument.rootVisualElement);
			List<T> a =_queryBuilder.OfType<T>().ToList();
			return a;
		}
	}
}