using UnityEngine;
using UnityEngine.UIElements;

namespace View.StartMenu.UIBuilder
{
	[RequireComponent(typeof(UIDocument))]
	public class UIElementGetterFacadeView : MonoBehaviour
	{
		[SerializeField] private UIDocument _uiDocument;

		private void Awake() =>
			_uiDocument = GetComponent<UIDocument>();

		public T GetUIElementQ<T>(string elementType) where T : VisualElement =>
			_uiDocument.rootVisualElement.Q<T>(elementType);

		public T GetUIElementQuery<T>(string elementType) where T : VisualElement =>
			_uiDocument.rootVisualElement.Query<T>(elementType);
	}
}