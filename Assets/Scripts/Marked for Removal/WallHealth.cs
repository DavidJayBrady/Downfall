using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health;
    bool startedDmg;
    void Start()
    {
        startedDmg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health<=0){
            Destroy(gameObject);
        } else if(!startedDmg && GetComponent<BoxCollider2D>().enabled){
            //takes 10 dmg every 2 seconds
            //StartCoroutine(takeDamage(10,2.0f));
            //startedDmg = true;
        }
    }

    IEnumerator takeDamage(int dmg, float delayTime){
		yield return new WaitForSeconds(delayTime);
        Health -= dmg;
        startedDmg = false;
    }
}
