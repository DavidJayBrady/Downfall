
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerController))]
public class DefenderController : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject underfootHighlighter;
    public GameObject selectionHighlighter;
    
    private Building selectedBuilding;
    public Building wallBuilding;
    public Building towerBuilding;
    public Building extractorBuilding;
    public Building researchBuilding;

    // Use this for initialization
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerLook();
        UpdateBuildingChoice();
        CheckPlayerBuilding();
    }

    // Returns true when the player is in camera mode
    bool IsCameraMode()
    {
        return Input.GetAxisRaw("Right Trigger") > 0.1f;
    }

    // Returns true when the player has a building selected and camera mode is off
    bool IsBuildMode()
    {
        return selectedBuilding != null && !IsCameraMode();
    }

    // Returns the position of the tile the player is on
    Vector3Int GetPlayerPosition()
    {
        return World.WorldToCell(this.transform.position);
    }

    // Returns the position of the tile in the direction the player is looking, or under the player if not looking
    Vector3Int GetPlayerSelection()
    {
        // Calculate the Vector3Int for the grid
        Vector2 lookVector = playerController.lookVector;
        Common.RotateVector2(ref lookVector, 45);
        float lookAngle = Common.VectorAngleDegrees(lookVector);
        return GetPlayerPosition() + Common.AngleToVector3Int(lookAngle);
    }

    // Update the direction the player is looking
    // Controlled by right joystick
    void UpdatePlayerLook()
    {
        underfootHighlighter.transform.position = World.CellToWorld(GetPlayerPosition());
        if (IsBuildMode())
        {
            selectionHighlighter.SetActive(true);
            underfootHighlighter.SetActive(true);
            selectionHighlighter.transform.position = World.CellToWorld(GetPlayerSelection());
            underfootHighlighter.transform.position = World.CellToWorld(GetPlayerPosition());
        }
        else
        {
            selectionHighlighter.SetActive(false);
            underfootHighlighter.SetActive(false);
        }
    }

    // See if the player picked a different structure to build
    void UpdateBuildingChoice()
    {
        if (Input.GetAxisRaw("D-Pad Horizontal") < -0.5f) // Left
        {
            selectedBuilding = wallBuilding;
        }
        else if (Input.GetAxisRaw("D-Pad Vertical") > 0.5f) // Up
        {
            selectedBuilding = towerBuilding;
        }
        else if (Input.GetAxisRaw("D-Pad Horizontal") > 0.5f) // Right
        {
            selectedBuilding = extractorBuilding; // extractorBuilding
        }
        else if (Input.GetAxisRaw("D-Pad Vertical") < -0.5f) // Down
        {
            selectedBuilding = researchBuilding; // researchBuilding
        }
        else if (Input.GetAxisRaw("B Button") > 0.5f)
        {
            selectedBuilding = null;
        }
    }

    // Check if the player built a structure
    void CheckPlayerBuilding()
    {
        if (IsBuildMode())
        {
            if (Input.GetAxisRaw("A Button") > 0.5f)
            {
                World.Instance.buildingManager.BuildAt(GetPlayerSelection(), selectedBuilding);
            }
        }
    }
}
