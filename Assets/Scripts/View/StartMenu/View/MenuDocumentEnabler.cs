using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace View.StartMenu.View
{
	public class MenuDocumentEnabler : MonoBehaviour
	{
		[SerializeField] private Button _menuCanvasButton;
		[SerializeField] private UIDocument _desiredWindow;
		[SerializeField] private GameObject _closedWindow;

		private void OnEnable() =>
			_menuCanvasButton.onClick.AddListener(OnButtonClicked);

		private void OnDisable() =>
			_menuCanvasButton.onClick.RemoveListener(OnButtonClicked);

		private void OnButtonClicked()
		{
			_closedWindow.gameObject.SetActive(false);
			_desiredWindow.enabled = true;
		}
	}
}