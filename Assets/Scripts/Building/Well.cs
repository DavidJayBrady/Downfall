using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public int initialResourceCount = 400;
    private int resourceCount;

    // Start is called before the first frame update
    void Start()
    {
        resourceCount = initialResourceCount;
        World.Instance.wellManager.RegisterWell(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetWellSize()
    {
        return resourceCount;
    }

    // Returns the amount that can be extracted
    public int Extract(int amount)
    {
        if (amount > resourceCount)
        {
            int result = resourceCount;
            resourceCount = 0;
            return result;
        }
        else
        {
            resourceCount -= amount;
            return amount;
        }
    }
}
