using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage functions related to the match; ie match time, win conditions, etc
public class MatchManager : MonoBehaviour
{
    private GameObject[] defenders;
    public int defenderResources = 0;

   
    public static float researchProgress = 0.0f;
    //public float setResearchProgress = 0.0f;

    private float matchTimeStart;
    // Time in seconds since the match started
    public float matchTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        matchTimeStart = Time.time;
        //researchProgress = 0.0f;
        defenders = GameObject.FindGameObjectsWithTag("Defender");
    }

    // Update is called once per frame
    void Update()
    {
        matchTime = Time.time - matchTimeStart;
        if(DidDefendersWin()){
            Debug.Log("Defenders WON!");
        }
        else if (DidAttackerWin())
        {
            Debug.Log("Attacker WON!");
        }
    }

    // Adds defender resources
    public void AddResources(int amount)
    {
        defenderResources += amount;
    }

    // Returns true when the defenders have enough 
    public bool HasResources(int amount)
    {
        return defenderResources >= amount;
    }

    // Subtracts defender resources
    public bool RemoveResources(int amount)
    {
        if (HasResources(amount))
        {
            defenderResources -= amount;
            return true;
        }
        return false;
    }

    // Adds research progress
    public void AddResearch(float amount)
    {
        researchProgress += amount;
        Debug.Log("Research Progress: " + researchProgress);
    }

    public void ResetResearch(){
        researchProgress = 0.0f;
    }
        
    // Return true when research progress surpasses 100%
    public bool DidDefendersWin()
    {
        return researchProgress >= 100.0f;
    }

    // Return true when both defenders are dead
    public bool DidAttackerWin()
    {
        foreach (GameObject defender in defenders)
        {
            if (defender != null)
                return false;
        }
        return true;
    }
}
