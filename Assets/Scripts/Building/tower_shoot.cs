using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_shoot : MonoBehaviour
{

    public int damage;
    public AttackerController AC;
    public float attack_delay;
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
       // Debug.Log("Tower");
        //AC.towerHit();
    }

    void OnTriggerEnter2D (Collider2D Attacker){
        AC.towerHit(damage);
    }
    void OnTriggerStay2D (Collider2D other){
        Debug.Log("in collider");
        //AC.towerHit();
    }
}
