
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControllerMovement : MonoBehaviour {

    private float speed = 5.0f;
    private float cameraSpeed = 15.0f;

    public GameObject underfootHighlighter;
    public GameObject selectionHighlighter;
    public GameObject _camera; // Named with an underscore to avoid issues with Component.camera

    private Building selectedBuilding;
    public Building wallBuilding;
    public Building towerBuilding;

    private Vector2 lookVector = new Vector2(-1.0f, -1.0f);
    private Rigidbody2D _rigidbody2D; // Named with an underscore to avoid issues with Component.rigidbody2D

    // Use this for initialization
    void Start () {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        selectedBuilding = wallBuilding;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlayerVelocity();
        UpdatePlayerLook();
        UpdateCamera();
        UpdateBuildingChoice();
        CheckPlayerBuilding();
    }

    // Update the player's velocity
    void UpdatePlayerVelocity()
    {
        _rigidbody2D.velocity = Common.GetScaledVectorInput(1, "Left Horizontal", "Left Vertical", speed) * new Vector2(1.0f, 0.5f);
    }
    
    // Returns true when the player is in camera mode
    bool IsCameraMode()
    {
        return Common.GetControllerInputAxis(1, "Right Trigger") > 0.1f;
    }

    // Update the camera's position and/or velocity
    void UpdateCamera()
    {
        if (IsCameraMode())
        {
            // Detach the camera from the player
            _camera.transform.parent = null;
            Vector3 deltaPos = Common.GetScaledVectorInput(1, "Right Horizontal", "Right Vertical", cameraSpeed * Time.deltaTime) * new Vector2(1.0f, 0.5f);
            _camera.transform.position += deltaPos;
        }
        else
        {
            // reattach the camera to the player
            _camera.transform.parent = this.transform;
            _camera.transform.localPosition = new Vector3(0.0f, 0.0f, -19.0f); // z=-19 is default
        }
    }

    // Returns the position of the tile the player is on
    Vector3Int GetPlayerPosition()
    {
        return World.WorldToCell(this.transform.position);
    }

    // Returns the position of the tile in the direction the player is looking, or under the player if not looking
    Vector3Int GetPlayerSelection()
    {
        // If the input is over half way to full, set the player direction
        Vector2 tempLookVector;
        if (!IsCameraMode())
        {
            tempLookVector = Common.GetScaledVectorInput(1, "Right Horizontal", "Right Vertical"); ;
            if (tempLookVector.magnitude > 0.5f)
            {
                Common.VectorNormalize(ref tempLookVector);
                lookVector = tempLookVector;
            }
        }
        // Calculate the Vector3Int for the grid
        tempLookVector = lookVector;
        Common.RotateVector2(ref tempLookVector, 45);
        float lookAngle = Common.VectorAngleDegrees(tempLookVector);
        return GetPlayerPosition() + Common.AngleToVector3Int(lookAngle);
    }

    // Update the direction the player is looking
    // Controlled by right joystick
    void UpdatePlayerLook()
    {
        underfootHighlighter.transform.position = World.CellToWorld(GetPlayerPosition());
        selectionHighlighter.transform.position = World.CellToWorld(GetPlayerSelection());
    }

    // See if the player picked a different structure to build
    void UpdateBuildingChoice()
    {
        if (Common.GetControllerInputAxis(1, "D-Pad Horizontal") < -0.5f) // Left
        {
            selectedBuilding = wallBuilding;
        }
        else if (Common.GetControllerInputAxis(1, "D-Pad Vertical") > 0.5f) // Up
        {
            selectedBuilding = towerBuilding;
        }
        else if (Common.GetControllerInputAxis(1, "D-Pad Horizontal") > 0.5f) // Right
        {
            selectedBuilding = wallBuilding; // extractorBuilding
        }
        else if (Common.GetControllerInputAxis(1, "D-Pad Vertical") < -0.5f) // Down
        {
            selectedBuilding = wallBuilding; // researchBuilding
        }
    }

    // Check if the player built a structure
    void CheckPlayerBuilding()
    {
        if (Common.GetControllerInputAxis(1, "A Button") > 0.5f)
        {
            World.Instance.buildingManager.BuildAt(GetPlayerSelection(), selectedBuilding);
        }
    }
}
