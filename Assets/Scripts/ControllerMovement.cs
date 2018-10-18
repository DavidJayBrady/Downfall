
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {

    private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlayerVelocity();
    }

    // Update the player's velocity
    // Controlled with WASD, arrow keys, and the left joystick
    // Considers ~90+% joystick input to be full speed
    void UpdatePlayerVelocity()
    {
        float effectiveSpeed = Mathf.Min(1.0f, Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw("Horizontal") * 1.1f, 2) + Mathf.Pow(Input.GetAxisRaw("Vertical") * 1.1f, 2))) * speed;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            this.GetComponent<Rigidbody2D>().velocity *= effectiveSpeed / this.GetComponent<Rigidbody2D>().velocity.magnitude;
    }
}
