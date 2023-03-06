using UnityEditor;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.Helpers
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEngine.GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            UnityEngine.GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
