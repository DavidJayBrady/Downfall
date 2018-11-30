using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{

    public int damage;
    public AttackerController attackerController;
    public float attackDelay;
    private float timer = 0.0f;

    static private bool attackerInRange;

    void Start()
    {
        attackerInRange = false;
    }

    // Update is called once per frame
     void Update()
    {
       timer = timer + Time.deltaTime;
       if(attackerInRange && timer >= attackDelay){
            attackerController.TowerHit(damage);
            Debug.Log("towerHitAttacker");
            timer = 0.0f; 
       }
    }

    void OnTriggerEnter2D (Collider2D attacker){
        if (attacker.CompareTag("Attacker")){

            Debug.Log("entered collider");
            attackerInRange = true;
        }

    }
    void OnTriggerStay2D (Collider2D attacker){
        
        if (attacker.CompareTag("Attacker")/* && timer >= attackDelay*/){
            attackerInRange = true;
            /* 
            attackerController.TowerHit(damage);
            Debug.Log("towerHitAttacker");
            timer = 0.0f;
            */
        }
    }
    void OnTriggerExit2D (Collider2D attacker){
        if (attacker.CompareTag("Attacker")){
            Debug.Log("exited collider");
            attackerInRange = false;
        }
    }
}
