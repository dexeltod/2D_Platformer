using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButtonQuit : MonoBehaviour
{
	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable() => 
		_button.onClick.AddListener(PlayButtonPressed);

	private void OnDisable() => 
		_button.onClick.RemoveListener(PlayButtonPressed);

	private void PlayButtonPressed()
	{
		Application.Quit();
	}
}