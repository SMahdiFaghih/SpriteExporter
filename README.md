**Available on Asset Store:** https://assetstore.unity.com/packages/tools/sprite-management/sprite-exporter-293562

# Overview
The SpriteExporter script is a Unity Editor tool designed to Export sliced sprites from a specified Texture2D into individual PNG files. This can be particularly useful for extracting and saving individual sprites from a Sprite Sheet.

## How to Use
* Ensure the SpriteExporter script is placed in the **Assets** folder within your Unity project.

* Update the **spritePath** variable in the **ExportSlicedSprites** method to point to the texture containing the sliced sprites you want to export. You can get the path by right-clicking on the texture and selecting "Copy Path".

* Ensure that the **Read/Write** property of the texture is enabled and the TextureType property of the texture is **Sprite (2D and UI)**.

* In the Unity Editor, go to the menu bar and select **Tools > Export Sliced Sprites**. This will execute the script and export the sliced sprites.
