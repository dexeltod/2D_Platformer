using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace View.CustomEditor
{
	[UnityEditor.CustomEditor(typeof(PlayerProgressView))]
	public class CustomProgressViewEditor : Editor
	{
		private const string SetStartProgress = "Set start progress";

		public override void OnInspectorGUI()
		{
			PlayerProgressView progressView = (PlayerProgressView)target;

			if (GUILayout.Button(SetStartProgress))
				progressView.SetStartProgress();
		}
	}
}
#endif