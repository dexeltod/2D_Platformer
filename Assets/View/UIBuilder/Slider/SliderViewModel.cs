using System;
using UnityEngine;

namespace View.UIBuilder.Slider
{
	public class SliderViewModel : MonoBehaviour
	{
		private UIElementGetterView _uiElementGetterView;

		private void Awake() => 
			_uiElementGetterView = GetComponent<UIElementGetterView>();

		private void OnEnable()
		{
		}
	}
}