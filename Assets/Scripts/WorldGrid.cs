using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides functions related to grid to world and world to grid interactions
public class WorldGrid
{
    static public GameObject world = GameObject.FindWithTag("World");
    static private GridLayout grid = world.GetComponent<GridLayout>();

    // Return the world position from the world position
    public static Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return grid.WorldToCell(worldPosition);
    }

    // Return the grid position from the grid position
    // TODO: Rewrite this function to ensure it works properly
    public static Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return grid.CellToWorld(cellPosition) + Vector3.Scale(new Vector3(0.0f, 0.5f, 0.0f), grid.cellSize);
    }

    // Converts a Vector2 to follow the coordinates of the grid
    public static Vector2 GetGridVector(Vector2 rawVector)
    {
        Common.RotateVector2(ref rawVector, 45);
        return rawVector * new Vector2(1.0f, 0.5f);
    }
}
