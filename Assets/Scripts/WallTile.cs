using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : BuildingTile
{
    public override int maxHealth { get { return 100; } }
    public override EnumBuildingType buildingType { get { return EnumBuildingType.Wall; } }

    public override void BuildingUpdate(Vector3Int location, WorldTilemap worldTilemap)
    {
        health -= 1;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/WallTile Asset")]
    public static void CreateWallTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Wall Tile", "WallTile", "Asset", "Save Wall Tile", "Assets/Tiles");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildingTile>(), path);
    }
#endif
}
