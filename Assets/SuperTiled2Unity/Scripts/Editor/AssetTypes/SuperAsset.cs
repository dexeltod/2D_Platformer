﻿using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2020_2_OR_NEWER
using AssetImportContext = UnityEditor.AssetImporters.AssetImportContext;
#else
using AssetImportContext = UnityEditor.Experimental.AssetImporters.AssetImportContext;
#endif

namespace SuperTiled2Unity.Scripts.Editor.AssetTypes
{
    // SuperAssets give us the ability to search for Tiled asset types
    public class SuperAsset : ScriptableObject
    {
        [SerializeField]
        private List<string> m_AssetDependencies = new List<string>();
        public List<String> AssetDependencies { get { return m_AssetDependencies; } }

        public void AddDependency(AssetImportContext context, string assetPath)
        {
            if (!m_AssetDependencies.Contains(assetPath))
            {
#if UNITY_2018_3_OR_NEWER
                context.DependsOnSourceAsset(assetPath);
#endif
                m_AssetDependencies.Add(assetPath);
            }
        }
    }
}
