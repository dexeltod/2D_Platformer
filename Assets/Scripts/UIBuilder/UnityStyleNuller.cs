using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIBuilder
{
	[RequireComponent(typeof(UIDocument))]
	public class UnityStyleNuller : MonoBehaviour
	{
		[SerializeField] private bool _isNullSlidersStyle;
		
		private UIDocument _document;
		private const string SliderInput = "SoundSlider";

		private VisualElement _root;

		private void Awake()
		{
			_document = GetComponent<UIDocument>();
			_root = _document.rootVisualElement;

			NullAllSlidersStyle();
		}

		private void NullAllSlidersStyle()
		{
			if (_isNullSlidersStyle == false)
				return;
			
			List<Slider> sliders = _root.Query<Slider>(SliderInput).ToList();

			foreach (var slider in sliders) 
				slider.focusable = false;
		}
	}
}