using UnityEngine;
using UnityEngine.UI;

namespace View.StartMenu.View
{
	public class MenuCanvasEnabler : MonoBehaviour
	{
		[SerializeField] private Button _menuCanvasButton;
		[SerializeField] private GameObject _desiredWindow;
		[SerializeField] private GameObject _closedWindow;

		private void OnEnable() =>
			_menuCanvasButton.onClick.AddListener(OnButtonClicked);

		private void OnDisable() =>
			_menuCanvasButton.onClick.RemoveListener(OnButtonClicked);

		private void OnButtonClicked()
		{
			_closedWindow.gameObject.SetActive(false);
			_desiredWindow.gameObject.SetActive(true);
		}
	}
}