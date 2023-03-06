using UnityEditor;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.Postprocessors
{
    public class SuperTexturePostprocessor : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (assetImporter.importSettingsMissing)
            {
                // The texture is being imported for the first time
                // Give the imported texture better defaults than provided by stock Unity
                TextureImporter textureImporter = assetImporter as TextureImporter;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.mipmapEnabled = false;
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

                TextureImporterSettings settings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(settings);
                settings.spriteGenerateFallbackPhysicsShape = false;
                textureImporter.SetTextureSettings(settings);
            }
        }
    }
}
