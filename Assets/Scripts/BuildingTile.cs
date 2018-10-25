using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingTile : TileBase
{
    public Sprite sprite;

    public int health;
    public virtual int maxHealth { get { return 1; } }
    public virtual EnumBuildingType buildingType { get { return EnumBuildingType.BuildingBase; } }

    public enum EnumBuildingType { None, BuildingBase, Wall, Tower, Extractor, Research }

    // This is called once per frame by WorldTilemap
    public virtual void BuildingUpdate(Vector3Int location, WorldTilemap worldTilemap)
    {

    }

    // Provide information about what the sprite is
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
    }
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        Debug.Log("starting new building");
        health = maxHealth;
        return true;
    }

    public bool IsDestroyed()
    {
        return health <= 0;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/BuildingTile Asset")]
    public static void CreateBuildingTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Building Tile", "New Building Tile", "Asset", "Save Building Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildingTile>(), path);
    }
#endif
}
