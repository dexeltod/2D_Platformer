using Game.Environment.EnterTriggers.Enter;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Environment.EnterTriggers
{
	public class OnTriggerCanvasEnabler : MonoBehaviour
	{
		[SerializeField] private EnterInLocationByButtonTrigger _buttonTrigger;
		[SerializeField] private Image _buttonImage;

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
			_buttonImage.enabled = isInTrigger;
		}
	}
}