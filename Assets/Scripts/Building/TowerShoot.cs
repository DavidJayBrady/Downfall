using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{

    public int damage;
    public AttackerController attackerController;
    public float attackDelay;
    private float timer = 0.0f;

    void Start()
    {
    }

    // Update is called once per frame
     void Update()
    {
       timer = timer + Time.deltaTime;
    }

    void OnTriggerEnter2D (Collider2D attacker){
        Debug.Log("entered collider");

    }
    void OnTriggerStay2D (Collider2D attacker){
        if (attacker.CompareTag("Attacker") && timer >= attackDelay){
            attackerController.TowerHit(damage);
            timer = 0.0f;
        }
    }
    void OnTriggerExit2D (Collider2D attacker){
        Debug.Log("exited collider");

    }
}
