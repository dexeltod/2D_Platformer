using System;
using UnityEditor;

namespace SuperTiled2Unity.Scripts.Editor.GUI
{
    public class GuiScopedIndent : IDisposable
    {
        public GuiScopedIndent()
        {
            EditorGUI.indentLevel++;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel--;
        }
    }
}
