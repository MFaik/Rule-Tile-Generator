using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
     
public class SpriteSlicer : EditorWindow 
{
    [MenuItem("Assets/EditorTools/Slice Sprites")]
    static void SliceSprites()
    {
        (int sliceWidth, int sliceHeight, bool confirmed) = Open();
        if(!confirmed)
            return; 
        if(sliceWidth == 0 || sliceHeight == 0){
            Debug.LogError("Size must be greater than 0");
            return;
        }
        foreach (var spriteObject in Selection.objects) {
            Texture2D sprite = spriteObject as Texture2D;
            if(sprite.width < sliceWidth || sprite.height < sliceHeight){
                Debug.LogError("Slices are too big for the sprites");
                return;
            }
        }
        foreach (var spriteObject in Selection.objects) {
            Texture2D sprite = spriteObject as Texture2D;

            string path = AssetDatabase.GetAssetPath(spriteObject);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.isReadable = true;
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();

            for (int i = 0; i < sprite.width; i += sliceWidth)
            {
                for (int j = sprite.height; j > 0; j -= sliceHeight)
                {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(0.5f, 0.5f);
                    smd.alignment = 9;
                    smd.name = (sprite.height - j) / sliceHeight + ", " + i / sliceWidth;
                    smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);

                    newData.Add(smd);
                }
            }

            textureImporter.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }

    [MenuItem("Assets/EditorTools/Slice Sprites", true)]
    private static bool SliceSpritesValidation()
    {
        foreach(var selectObject in Selection.objects){
            if(selectObject is not Texture2D)
                return false;
        }
        
        return true;
    }

    public int InputWidth = 8,InputHeight = 8;
    public bool Confirmed = false;

    private void OnGUI() {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tile Width");
            InputWidth = EditorGUILayout.IntField(InputWidth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tile Height");
            InputHeight = EditorGUILayout.IntField(InputHeight);
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Ok")){
            Confirmed = true;
            Close();
        }
    }

    public static (int, int, bool) Open(){
        SpriteSlicer window = CreateInstance<SpriteSlicer>();
        window.maxSize = new Vector2(300f, 80f);
        window.minSize = window.maxSize;
        window.ShowModal();
        
        return (window.InputWidth, window.InputHeight, window.Confirmed);
    }
}
