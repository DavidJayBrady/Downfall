
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControllerMovement : MonoBehaviour {

    private float speed = 5.0f;
    public GameObject underfootHighlighter;
    public GameObject selectionHighlighter;
    public GameObject world;
    private GridLayout worldGrid;

	// Use this for initialization
	void Start () {
        worldGrid = world.GetComponent<GridLayout>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlayerVelocity();
        UpdatePlayerView();
    }

    // Return a vector representation of the joystick input
    // Interprets 
    Vector3 GetScaledVectorInput(string axisNameX, string axisNameY, float scale = 1.0f)
    {
        Vector3 result = new Vector3(Input.GetAxisRaw(axisNameX), Input.GetAxisRaw(axisNameY), 0.0f);
        float effectiveScale = Mathf.Min(1.0f, Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw(axisNameX) * 1.1f, 2) + Mathf.Pow(Input.GetAxisRaw(axisNameY) * 1.1f, 2))) * scale;
        if (result.magnitude > 0)
            result *= effectiveScale / result.magnitude;
        return result;
    }

    // Rotate a Vector2 by an angle clockwise
    Vector2 RotateVector2(Vector2 vector, float degrees)
    {
        float rad = -degrees * Mathf.Deg2Rad;
        float s = Mathf.Sin(rad);
        float c = Mathf.Cos(rad);
        return new Vector2( vector.x * c - vector.y * s, vector.y * c + vector.x * s);
    }

    // Update the player's velocity
    void UpdatePlayerVelocity()
    {
        this.GetComponent<Rigidbody2D>().velocity = GetScaledVectorInput("Horizontal", "Vertical", speed) * new Vector2(1.0f, 0.5f);
    }

    // Returns the position of the tile the player is on
    Vector3Int GetPlayerPosition()
    {
        return worldGrid.WorldToCell(this.transform.position);
    }

    // Returns the position of the tile in the direction the player is looking, or under the player if not looking
    Vector3Int GetPlayerSelection()
    {
        Vector3Int selectionVector = worldGrid.WorldToCell(this.transform.position);
        Vector2 lookVector = GetScaledVectorInput("Horizontal2", "Vertical2");
        lookVector = RotateVector2(lookVector, 45);
        selectionVector.x += Mathf.RoundToInt(lookVector.x);
        selectionVector.y += Mathf.RoundToInt(lookVector.y);
        return selectionVector;
    }

    // Update the direction the player is looking
    // Controlled by right joystick
    void UpdatePlayerView()
    {
        underfootHighlighter.transform.position = worldGrid.CellToWorld(GetPlayerPosition());
        selectionHighlighter.transform.position = worldGrid.CellToWorld(GetPlayerSelection());
    }
}
