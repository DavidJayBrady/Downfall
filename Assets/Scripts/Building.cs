using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private int health = 1;
    public int maxHealth = 1;
    public int upgradeLevel = 1;

    public int buildCost = 1;

    public enum BuildingType { None, Wall, Tower, Extractor, Research }
    public static BuildingType buildingType = BuildingType.None;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO remove when working
        Damage(1);
    }

    public void Damage(int amount)
    {
        health -= amount;
    }

    // Return true when the building is to be deleted (aka health <= 0)
    public bool IsDestroyed()
    {
        return health <= 0;
    }
}
