using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonView : MonoBehaviour
{
	[SerializeField] private Button _play;
	
	public event Action PlayButtonPressed;
	
	private void OnEnable()
	{
		_play.onClick.AddListener(OnPlay);
	}

	private void OnDisable()
	{
		_play.onClick.RemoveListener(OnPlay);
	}

	private void OnPlay()
	{
		PlayButtonPressed?.Invoke();
	}
}