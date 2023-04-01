using UnityEngine;
using UnityEngine.UI;

public class UIButtonSoundPlayer : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private AudioSource _audio;

	private void OnEnable()
	{
		_button.onClick.AddListener(OnButtonClicked);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(OnButtonClicked);
	}

	private void OnButtonClicked()
	{
		_audio.Play();
	}
}