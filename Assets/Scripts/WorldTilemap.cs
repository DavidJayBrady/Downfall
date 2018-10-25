using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTilemap : MonoBehaviour
{
    private Tilemap terrainTilemap;
    private GridInformation terrainProperties; // unused for now, but may be used for depleting resources in future
    private Tilemap buildingTilemap;
    public GridInformation buildingProperties;

    public GameObject terrainTilemapGameObject;
    public GameObject buildingTilemapGameObject;

    // Start is called before the first frame update
    void Start()
    {
        terrainTilemap = terrainTilemapGameObject.GetComponent<Tilemap>();
        buildingTilemap = buildingTilemapGameObject.GetComponent<Tilemap>();
        terrainTilemap.CompressBounds();
        buildingTilemap.CompressBounds();
        terrainProperties = terrainTilemapGameObject.GetComponent<GridInformation>();
        buildingProperties = buildingTilemapGameObject.GetComponent<GridInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check all buildings, remove them if they have no health, and update them if not.
        for (int i = buildingTilemap.cellBounds.xMin; i < buildingTilemap.cellBounds.xMax; i++)
        {
            for (int j = buildingTilemap.cellBounds.yMin; j < buildingTilemap.cellBounds.yMax; j++)
            {
                Vector3Int localPosition = (new Vector3Int(i, j, 0));
                TileBase tile = buildingTilemap.GetTile(localPosition);
                if (tile is BuildingTile)
                {
                    BuildingTile building = tile as BuildingTile;
                    if (building.IsDestroyed(localPosition, this))
                    {
                        buildingTilemap.SetTile(localPosition, null);
                    }
                    else
                    {
                        building.BuildingUpdate(localPosition, this);
                    }
                }
            }
        }
    }

    // ## BAD CODE START ##

    // TODO: update
    public enum EnumTerrainType { None, TerrainBase, Ground, Resource }
    // Returns the enum of the terrain layer
    public EnumTerrainType GetTerrainAt(Vector3Int location)
    {
        return EnumTerrainType.TerrainBase;
    }

    // ## BAD CODE END ##

    public BuildingTile.EnumBuildingType GetBuildingAt(Vector3Int location)
    {
        TileBase tile = buildingTilemap.GetTile(location);
        if (tile is BuildingTile)
        {
            return (tile as BuildingTile).buildingType;
        }
        else
        {
            return BuildingTile.EnumBuildingType.None;
        }
    }

    // Returns true when the position has terrain and no building
    public bool CanBuildAt(Vector3Int location)
    {
        return GetBuildingAt(location) == BuildingTile.EnumBuildingType.None;
    }

    // Tries to build at the location
    // Returns true on success
    public bool Build(Vector3Int location, BuildingTile building)
    {
        if (CanBuildAt(location))
        {
            buildingTilemap.SetTile(location, building);
            return true;
        }
        return false;
    }
}
