using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnGenerator : MonoBehaviour
{
    public GameObject GeneratorObject;
    public float buildDelay;

    public bool built;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float gPressed =Input.GetAxis("g");
        if(gPressed == 1 && !built){
            SpriteRenderer generatorSprite;
            generatorSprite = GeneratorObject.GetComponent<SpriteRenderer>();
            generatorSprite.enabled = true;
            generatorSprite.color = new Color32(255,255,255,120);
            BoxCollider2D generatorCollider;
            generatorCollider = GeneratorObject.GetComponent<BoxCollider2D>();
            StartCoroutine(solidify(generatorSprite,generatorCollider,buildDelay));

        }
    }
    IEnumerator solidify(SpriteRenderer GS, BoxCollider2D GC,float delayTime){
		yield return new WaitForSeconds(delayTime);
		//sets sprite color to solid after delay 
		GS.color = new Color32(255,255,255,255);

		//set generator collider to true after delay
		GC.enabled = true;
		
        built = true;
	}
}
