using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI_Scripts.Progress.View
{
	public class MenuButtonView : MonoBehaviour
	{
		[SerializeField] private Button _play;

		public event Action ButtonPressed;

		private void OnEnable() =>
			_play.onClick.AddListener(OnPlay);

		private void OnDisable() =>
			_play.onClick.RemoveListener(OnPlay);

		private void OnPlay() =>
			ButtonPressed?.Invoke();
	}
}