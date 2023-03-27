using UnityEngine;
using UnityEngine.UIElements;

namespace View.UIBuilder
{
	[RequireComponent(typeof(UIDocument))]
	public class UIElementGetterView : MonoBehaviour
	{
		[SerializeField] private UIDocument _uiDocument;

		private void Awake() => 
			_uiDocument = GetComponent<UIDocument>();

		public T GetUIElement<T>(string elementType) where T : VisualElement => 
			_uiDocument.rootVisualElement.Q<T>(elementType);
	}
}