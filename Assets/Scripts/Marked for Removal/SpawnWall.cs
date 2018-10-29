using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnWall : MonoBehaviour {
	GameObject selectedPrefab;
	public GameObject wallPrefab;
	public GameObject alternativeWall;
	public List<GameObject> allWalls = new List<GameObject>();
	public bool lastbPressed;
	public int buildDelay;
	Vector3 mousePos;
	// Use this for initialization
	void Start () {
		lastbPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//trying to change wall type on b pressed.
		float bPressed =Input.GetAxis("b");
		if(bPressed == 1){
			lastbPressed = !lastbPressed;
		}
		if(Input.GetMouseButtonDown(0)){
			//trying to change wall type on b pressed.
			if(lastbPressed ){
				selectedPrefab = alternativeWall;
			}else{
				selectedPrefab = wallPrefab;
			}

		
			//selectedPrefab = wallPrefab;

			//Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	mousePos.z = -5.0f;
			//if dont want to spawn right away, comment this v and uncomment code in SpawnW
			GameObject clone = Instantiate(selectedPrefab,mousePos,Quaternion.Euler(new Vector3(0,0,0)))as GameObject;
			spawner(clone);
			

				//nothing code vv
				//Vector2 mousePosV= new Vector2(mousePos.x,mousePos.y);
				//GameObject clone = new wallPrefab;
				//clone.transform.position = mousePos;
		}
	}
	void spawner(GameObject c){
		//gets and sets sprite color to light red on creation
		SpriteRenderer wall_sprite;
		wall_sprite = c.GetComponent<SpriteRenderer>();
		wall_sprite.color = new Color32(164,33,57,120);
		//gets collider of wall
		BoxCollider2D wall_collider;
		wall_collider = c.GetComponent<BoxCollider2D>();
		StartCoroutine(solidify(wall_sprite,wall_collider,buildDelay));
			//Invoke("solidify",2); ***doesnt work as well as corountine due to inability to send parameters
		allWalls.Add(c);
	}

	IEnumerator solidify(SpriteRenderer WS, BoxCollider2D WC,float delayTime){
		yield return new WaitForSeconds(delayTime);
		//sets sprite color to solid red after delay 
		WS.color = new Color32(164,33,57,255);

		//set wall collider to true after delay
		WC.enabled = true;
		

	}

}
