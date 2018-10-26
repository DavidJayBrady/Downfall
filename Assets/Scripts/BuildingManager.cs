using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Dictionary<Vector3Int, Building> Buildings = new Dictionary<Vector3Int, Building>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for buildings that are destroyed and remove them
        HashSet<Vector3Int> positionsToRemove = new HashSet<Vector3Int>();
        foreach (Vector3Int position in Buildings.Keys)
        {
            if (Buildings[position].IsDestroyed())
            {
                positionsToRemove.Add(position);
                Debug.Log(position);
            }
        }
        foreach (Vector3Int position in positionsToRemove)
        {
            Debug.Log(position);
            Destroy(Buildings[position].gameObject, 0);
            Buildings.Remove(position);
        }
    }

    // Does a building exist at the position
    public bool IsBuildingAt(Vector3Int position)
    {
        return Buildings.ContainsKey(position);
    }

    public Building GetBuildingAt(Vector3Int position)
    {
        if (!Buildings.ContainsKey(position))
            return null;
        return Buildings[position];
    }

    public bool BuildAt(Vector3Int position, Building buildingPrefab)
    {
        if (Buildings.ContainsKey(position))
            return false;
        if (World.Instance.matchManager.HasResources(buildingPrefab.buildCost))
            return false;
        Building building = Instantiate<Building>(buildingPrefab, World.CellToWorld(position), Quaternion.identity);
        World.Instance.matchManager.RemoveResources(buildingPrefab.buildCost);
        Buildings.Add(position, building);
        return true;
    }
}
