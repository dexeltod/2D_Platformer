using System;
using System.Collections.Generic;
using System.Linq;
using SuperTiled2Unity.Scripts.Editor.AssetTypes;
using UnityEditor;

namespace SuperTiled2Unity.Scripts.Editor.Windows
{
    public class RecursiveAssetDependencyTracker
    {
        private string m_SourceAsset;
        private HashSet<string> m_VisitedFiles = new HashSet<string>();
        private HashSet<string> m_Dependencies = new HashSet<string>();

        public RecursiveAssetDependencyTracker(string asset)
        {
            m_SourceAsset = asset;
            ProcessFile(m_SourceAsset);
        }

        public List<string> Dependencies { get { return m_Dependencies.ToList(); } }

        private void ProcessFile(string assetPath)
        {
            if (!m_VisitedFiles.Contains(assetPath, StringComparer.OrdinalIgnoreCase))
            {
                m_VisitedFiles.Add(assetPath);
                m_Dependencies.Add(assetPath);

                var super = AssetDatabase.LoadAssetAtPath<SuperAsset>(assetPath) as SuperAsset;
                if (super != null)
                {
                    foreach (var path in super.AssetDependencies)
                    {
                        ProcessFile(path);
                    }
                }
            }
        }
    }
}
