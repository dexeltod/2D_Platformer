using Game.Environment.EnterTriggers.Enter;
using UnityEngine;

public class OnTriggerCanvasEnabler : MonoBehaviour
{
	[SerializeField] private EnterInLocationByButtonTrigger _buttonTrigger;
	[SerializeField] private GameObject _button;

	private void OnEnable()
	{
		_buttonTrigger.InTriggerEntered += OnTriggerEntered;
	}

	private void OnDisable()
	{
		_buttonTrigger.InTriggerEntered -= OnTriggerEntered;
	}

	private void OnTriggerEntered(bool isInTrigger)
	{
		_button.SetActive(isInTrigger);
	}
}