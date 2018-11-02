using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_shoot : MonoBehaviour
{

    public int damage;
    public AttackerController AC;
    public float attack_delay;
    private float timer = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
       timer = timer + Time.deltaTime;
       // Debug.Log("Tower");
        //AC.towerHit();
    }

    void OnTriggerEnter2D (Collider2D Attacker){
        //AC.towerHit(damage);
        Debug.Log("entered collider");

    }
    void OnTriggerStay2D (Collider2D Attacker){
        if (timer >= attack_delay && Attacker.tag == "Attacker"){
            //Debug.Log("in collider");
            AC.towerHit(damage);
            timer = 0.0f;
        }
    }
    void OnTriggerExit2D (Collider2D Attacker){
        //AC.towerHit(damage);
        Debug.Log("exited collider");

    }
}
