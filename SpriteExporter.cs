using UnityEngine;
using UnityEditor;
using System.IO;

namespace SpriteExporter
{
    public class SpriteExporter
    {
        [MenuItem("Assets/SpriteExporter/ExportFromSpriteSheet")]
        public static void ExportSlicedSprites()
        {
            var selectedObject = Selection.activeObject;
            if (selectedObject == null)
            {
                Debug.LogError("No object selected! Please select an object in the Project view.");
                return;
            }

            string spriteSheetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(spriteSheetPath) || !spriteSheetPath.Contains(".png"))
            {
                Debug.LogError("The selected file is not a .png file. Please ensure a .png file is selected.");
                return;
            }
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(spriteSheetPath);

            string outputDirectoryPath = Path.Combine(Path.GetDirectoryName(spriteSheetPath), "ExportedSprites");
            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            //Get all the sliced sprites
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath);
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
                    File.WriteAllBytes(Path.Combine(outputDirectoryPath, sprite.name) + ".png", pngData);
                }
            }

            AssetDatabase.Refresh();
            Debug.Log("Sprites exported successfully!");
        }
    }
}