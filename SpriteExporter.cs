using UnityEngine;
using UnityEditor;
using System.IO;

namespace SpriteExporter
{
    public class SpriteExporter
    {
        [MenuItem("Tools/Export Sliced Sprites")]
        public static void ExportSlicedSprites()
        {
            string spritePath = "Assets/PathToYourSprite.png"; //Path to the sliced Sprite Sheet
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(spritePath);
            string outputDirectoryPath = "Assets/ExportedSprites/";

            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            //Get all the sliced sprites
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(spritePath);
            foreach (var asset in assets)
            {
                if (asset is not Sprite sprite)
                {
                    //If the path is correct, this indicates the Texture2D type which is the entire Sprite Sheet
                    continue;
                }

                //Create a new texture for each sprite
                Texture2D newTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] pixels = texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
                newTexture.SetPixels(pixels);
                newTexture.Apply();

                //Convert the texture to PNG
                byte[] pngData = newTexture.EncodeToPNG();
                if (pngData != null)
                {
                    File.WriteAllBytes(outputDirectoryPath + sprite.name + ".png", pngData);
                }
            }

            AssetDatabase.Refresh();
            Debug.Log("Sprites exported successfully!");
        }
    }
}