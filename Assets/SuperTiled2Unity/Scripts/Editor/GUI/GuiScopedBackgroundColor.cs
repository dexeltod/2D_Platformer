using System;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.GUI
{
    public class GuiScopedBackgroundColor : IDisposable
    {
        private Color m_DefaultColor;

        public GuiScopedBackgroundColor(Color color)
        {
            m_DefaultColor = UnityEngine.GUI.backgroundColor;
            UnityEngine.GUI.backgroundColor = color;
        }

        public void Dispose()
        {
            UnityEngine.GUI.backgroundColor = m_DefaultColor;
        }
    }
}
