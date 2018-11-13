using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Researcher : MonoBehaviour
{

    public MatchManager matchManager;

    public float timer;

    public float progressDelay;
    //progressDelay gets decreased when research center is upgraded

    //likewise, progressAmount can be increased when research center is updated.. up to designer which gets changed, same effect tho
    public float progressAmount;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        timer += Time.deltaTime;

        if(timer >= progressDelay){
            //World.Instance.matchManager.AddResearch(progressAmount);
            matchManager.AddResearch(progressAmount);
            //Debug.Log("Adding Research Progress: " + progressAmount);

            timer = 0.0f;
        }
            }


}
