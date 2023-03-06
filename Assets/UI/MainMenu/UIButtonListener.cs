using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
	public class UIButtonListener : MonoBehaviour
	{
		[SerializeField] private UIDocument _uiDocument;

		private Button _playButton;
		private const string Play = "Play";

		private void Awake()
		{
			_playButton = _uiDocument.rootVisualElement.Q(Play) as Button;
			_playButton.RegisterCallback<ClickEvent>(Test);
		}

		private void Test(ClickEvent evt)
		{
			Debug.Log("Хуй");
		}
	}
}