using System;
using System.IO;
using SuperTiled2Unity.Scripts.Editor.Extensions;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor
{
    public static class AssetPath
    {
        // Transform an absolute path into Assets/Path/To/Asset.ext
        public static bool TryAbsoluteToAsset(ref string path)
        {
            path = path.SanitizePath();

            if (path.StartsWith(Application.dataPath, StringComparison.OrdinalIgnoreCase))
            {
                path = path.Remove(0, Application.dataPath.Length + 1);
                path = Path.Combine("Assets/", path);
                path = path.SanitizePath();
                return true;
            }

            return false;
        }
    }
}
