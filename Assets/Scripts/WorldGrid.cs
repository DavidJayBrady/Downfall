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
    public static Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return grid.CellToWorld(cellPosition);
    }

    // Converts a Vector3 to follow the coordinates of the grid
  /*  public static Vector3 GetGridVector(Vector3 rawVector)
    {

    }*/
}
