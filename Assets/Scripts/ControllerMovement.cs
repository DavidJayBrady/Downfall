
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControllerMovement : MonoBehaviour {

    private float speed = 5.0f;

    public GameObject underfootHighlighter;
    public GameObject selectionHighlighter;

    private Vector2 lookVector = new Vector2(-1.0f, -1.0f);
    private Rigidbody2D _rigidbody2D; // Named with an underscore to avoid issues with Component.rigidbody2D

    // Use this for initialization
    void Start () {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlayerVelocity();
        UpdatePlayerView();
    }

    // Update the player's velocity
    void UpdatePlayerVelocity()
    {
        _rigidbody2D.velocity = Common.GetScaledVectorInput("Horizontal", "Vertical", speed) * new Vector2(1.0f, 0.5f);
    }

    // Returns the position of the tile the player is on
    Vector3Int GetPlayerPosition()
    {
        return WorldGrid.WorldToCell(this.transform.position);
    }

    // Returns the position of the tile in the direction the player is looking, or under the player if not looking
    Vector3Int GetPlayerSelection()
    {
        // If the input is over half way to full, set the player direction
        Vector2 tempLookVector = Common.GetScaledVectorInput("Horizontal2", "Vertical2");
        if (tempLookVector.magnitude > 0.5f)
        {
            tempLookVector = Common.RotateVector2(ref tempLookVector, 45);
            lookVector = tempLookVector;
        }
        print(lookVector);
        // Calculate the Vector3Int for the grid
        Vector3Int selectionVector = GetPlayerPosition();
        selectionVector.x += Mathf.RoundToInt(lookVector.x);
        selectionVector.y += Mathf.RoundToInt(lookVector.y);
        return selectionVector;
    }

    // Update the direction the player is looking
    // Controlled by right joystick
    void UpdatePlayerView()
    {
        underfootHighlighter.transform.position = WorldGrid.CellToWorld(GetPlayerPosition());
        selectionHighlighter.transform.position = WorldGrid.CellToWorld(GetPlayerSelection());
    }
}
