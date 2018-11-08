using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorController : MonoBehaviour
{
    // Start is called before the first frame update
    public int gold;
    public int goldGenerated;
    public float generationFrequency;
    public int health;

    bool startedGenerate;
    void Start()
    {
        startedGenerate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0){
            Destroy(gameObject);
        } else if(!startedGenerate && GetComponent<BoxCollider2D>().enabled){
            StartCoroutine(generateGold(goldGenerated,generationFrequency));
            startedGenerate = true;

        }
    }

    IEnumerator generateGold(int g, float freq){
        		yield return new WaitForSeconds(freq);
                gold+=g;
                startedGenerate = false;
    }
}
