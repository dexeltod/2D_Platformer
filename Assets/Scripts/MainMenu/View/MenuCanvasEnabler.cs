using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasEnabler : MonoBehaviour
{
	[SerializeField] private Button _menuCanvasButton;
	[SerializeField] private Canvas _desiredWindow;
	[SerializeField] private Canvas _closedWindow;

	private void OnEnable() =>
		_menuCanvasButton.onClick.AddListener(OnButtonClicked);

	private void OnDisable() =>
		_menuCanvasButton.onClick.RemoveListener(OnButtonClicked);

	private void OnButtonClicked()
	{
		_closedWindow.enabled = false;
		_desiredWindow.enabled = true;
	}
}