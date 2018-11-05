using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private int _health;
    public int maxHealth = 200;
    public int upgradeLevel = 1;

    public int buildCost = 1;

    public enum BuildingType { None, Wall, Tower, Extractor, Research }
    public static BuildingType buildingType = BuildingType.None;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_health);
        if (IsDestroyed())
        {
            Destroy(gameObject);
        }
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
