using System.Xml.Linq;
using SuperTiled2Unity.Scripts.Editor.GUI;
using UnityEditor;

namespace SuperTiled2Unity.Scripts.Editor.Importers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TxAssetImporter))]
    public class TxAssetImporterEditor : SuperImporterEditor<TxAssetImporter>
    {
        public override bool showImportedObject { get { return false; } }

        protected override string EditorLabel { get { return "Template importer (.tx files)"; } }

        protected override string EditorDefinition
        {
            get
            {
                return "This imports Tiled Map Editor template files (.tx) into Unity projects.\n" +
                    "TX assets are referenced by objects in Tiled Map (.tmx) assets.";
            }
        }

        public override bool HasPreviewGUI()
        {
            return false;
        }

        protected override void InternalOnInspectorGUI()
        {
            EditorGUILayout.LabelField("Template Xml Data", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            var objectTemplate = GetAssetTarget<ObjectTemplate>();
            if (objectTemplate != null)
            {
                XElement xml = XElement.Parse(objectTemplate.m_ObjectXml);
                InspectorGUIForXmlElement(xml);
            }

            ApplyRevertGUI();
        }

        private void InspectorGUIForXmlElement(XElement xml)
        {
            EditorGUILayout.LabelField(xml.Name.LocalName);

            using (new GuiScopedIndent())
            {
                UnityEngine.GUI.enabled = false;
                foreach (var a in xml.Attributes())
                {
                    EditorGUILayout.TextField(a.Name.LocalName, a.Value);
                }
                UnityEngine.GUI.enabled = true;

                foreach (XElement x in xml.Elements())
                {
                    InspectorGUIForXmlElement(x);
                }
            }
        }
    }
}
