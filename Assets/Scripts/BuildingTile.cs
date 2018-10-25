using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingTile : Tile
{
    public GridInformation buildingProperties; // This is very bad practice but works
    public virtual int maxHealth { get { return 1; } }
    public virtual EnumBuildingType buildingType { get { return EnumBuildingType.BuildingBase; } }

    public enum EnumBuildingType { None, BuildingBase, Wall, Tower, Extractor, Research }

    // This is called once per frame by WorldTilemap
    public virtual void BuildingUpdate(Vector3Int location, WorldTilemap worldTilemap)
    {

    }
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        colliderType = Tile.ColliderType.Grid;
        buildingProperties = WorldGrid.worldTilemap.buildingProperties;
        buildingProperties.SetPositionProperty(position, "health", maxHealth);
        return true;
    }

    public bool IsDestroyed(Vector3Int location, WorldTilemap worldTilemap)
    {
        return buildingProperties.GetPositionProperty(location, "health", 0) <= 0;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create an Asset
    [MenuItem("Assets/Create/BuildingTile Asset")]
    public static void CreateBuildingTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Building Tile", "BuildingTile", "Asset", "Save Building Tile", "Assets/Tiles");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildingTile>(), path);
    }
#endif
}
