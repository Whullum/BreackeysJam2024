using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomSpriteImporter : AssetPostprocessor
{
    
    void OnPreprocessTexture()
    {
        if (!assetPath.ToLower().Contains("sprite")) return;
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePixelsPerUnit = 16;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.mipmapEnabled = true;
    }
    
    
}
