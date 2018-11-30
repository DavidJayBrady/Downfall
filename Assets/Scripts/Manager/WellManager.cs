using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellManager : MonoBehaviour
{
    public Dictionary<Vector3Int, Well> Wells = new Dictionary<Vector3Int, Well>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("IsWellAt " + IsWellAt(new Vector3Int(2, 2, 0)));
        Debug.Log("GetWellAmountAt " + GetWellAmountAt(new Vector3Int(2, 2, 0)));
        Debug.Log("ExtractAmountFromWellAt " + ExtractAmountFromWellAt(new Vector3Int(2, 2, 0), 1));
    }

    // Does a building exist at the position
    public bool IsWellAt(Vector3Int position)
    {
        return Wells.ContainsKey(position);
    }

    public int GetWellAmountAt(Vector3Int position)
    {
        if (!Wells.ContainsKey(position))
            return 0;
        return Wells[position].GetWellSize();
    }

    // Returns the amount that could be extracted
    public int ExtractAmountFromWellAt(Vector3Int position, int amount)
    {
        if (!Wells.ContainsKey(position))
            return 0;
        return Wells[position].Extract(amount);
    }

    public void RegisterWell(Well well)
    {
        Vector3Int position = World.WorldToCell(well.transform.position);
        // Only allow one well per tile
        if (Wells.ContainsKey(position))
            return;
        Wells.Add(position, well);
    }
}
