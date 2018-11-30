using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides functions related to grid to world and world to grid interactions
[RequireComponent(typeof(GridLayout))]
[RequireComponent(typeof(BuildingManager))]
[RequireComponent(typeof(MatchManager))]
public class World : MonoBehaviour
{
    public static World Instance;
    private GridLayout grid;
    public BuildingManager buildingManager;
    public MatchManager matchManager;
    public WellManager wellManager;

    // Return the world
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            grid = GetComponent<GridLayout>();
            buildingManager = GetComponent<BuildingManager>();
            matchManager = GetComponent<MatchManager>();
            wellManager = GetComponent<WellManager>();
        }
    }

    // Return the world position from the world position
    public static Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return Instance.grid.WorldToCell(worldPosition);
    }

    // Return the grid position from the grid position
    // TODO: Rewrite this function to ensure it works properly
    public static Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return Instance.grid.CellToWorld(cellPosition) + Vector3.Scale(new Vector3(0.0f, 0.5f, 0.0f), Instance.grid.cellSize);
    }

    // Converts a Vector2 to follow the coordinates of the grid
    public static Vector2 GetGridVector(Vector2 rawVector)
    {
        Common.RotateVector2(ref rawVector, 45);
        return rawVector * new Vector2(1.0f, 0.5f);
    }
}
