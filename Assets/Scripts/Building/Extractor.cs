using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Extractor : MonoBehaviour
{
public int goldAmount;
public float delay;

private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay){
        // TODO
            World.Instance.matchManager.AddResources(goldAmount);
            timer = 0.0f;
        }
    }
}
