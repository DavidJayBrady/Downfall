using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

// Inherits from MonoBehaviour, Imlements CanFeelThePain
// INTERFACES MUST COME AFTER THE CLASS BEING INHERITED FROM
// Source: https://stackoverflow.com/questions/14139097/c-sharp-inheritance-implements-extends
public class Building : MonoBehaviour, Interfaces.CanFeelThePain
{
    private int _health;
    public int maxHealth = 200;
    public int upgradeLevel = 1;

    public int buildCost = 1;

    public int specificBuildCost;

    public enum BuildingType { None, Wall, Tower, Extractor, Research }
    public static BuildingType buildingType = BuildingType.None;

    // Start is called before the first frame update
    void Awake()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health);
    }

    public void Damage(int amount)
    {
        Debug.Log(_health);
        _health -= amount;
    }

    // Return true when the building is to be deleted (aka _health <= 0)
    public bool IsDestroyed()
    {
        return _health <= 0;
    }
}
