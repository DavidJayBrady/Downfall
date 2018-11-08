using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Researcher : MonoBehaviour
{

    public MatchManager matchManager;

    // Start is called before the first frame update
    void Start()
    {
       // matchManager.matchTimeStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
     //   World.Instance.matchManager.AddResearch(0.01f);
    }
}
