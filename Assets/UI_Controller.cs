using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerController))]
public class UI_Controller : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject underfootHighlighter;
    public GameObject selectionHighlighter;

    public Text TowerText;
    public Text WallText;
    public Text ExtractorText;
    public Text ResearchText;

    public Text TowerChoice1;
    public Text TowerChoice2;
    public Text TowerChoice3;

    public Text WallChoice1;
    public Text WallChoice2;

    public Text CurrentSelectT;

    private Building selectedBuilding;
    public Building wallBuilding;
    public Building towerBuilding;
    public Building extractorBuilding;
    public Building researchBuilding;

    bool ChoosingBuilding = true;
    bool ChoosingTower = false;
    bool ChoosingWall = false;

    int TowerSelection = 0;
    int WallSelection = 0;
    int BuildingSelection = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        WallChoice1.enabled = true;
        WallChoice2.enabled = true;
        TowerChoice1.enabled = true;
        TowerChoice2.enabled = true;
        TowerChoice3.enabled = true;
        WallChoice1.enabled = false;
        WallChoice2.enabled = false;
        TowerChoice1.enabled = false;
        TowerChoice2.enabled = false;
        TowerChoice3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerLook();
        //UpdateBuildingChoice();
        CheckPlayerBuilding();

        if (ChoosingBuilding == true)
        {
            BuildingSelect();
        }
        else if (ChoosingTower == true)
        {
            TowerSelect();   
        }
        else if (ChoosingWall == true)
        {
            WallSelect();
        }

    }

    public bool IsBuildMode()
    {
        return selectedBuilding != null && !playerController.IsCameraMode();
    }

    // Returns the position of the tile the player is on
    public Vector3Int GetPlayerPosition()
    {
        return World.WorldToCell(this.transform.position);
    }

    // Returns the position of the tile in the direction the player is looking
    public Vector3Int GetPlayerSelection()
    {
        // Calculate the Vector3Int for the grid
        // TODO investigate bug where this function always returns up (1,1,0)
        Vector2 lookVector = playerController.lookVector;
        Common.RotateVector2(ref lookVector, 45);
        float lookAngle = Common.VectorAngleDegrees(lookVector);
        return GetPlayerPosition() + Common.AngleToVector3Int(lookAngle);
    }

    // Update the direction the player is looking
    private void UpdatePlayerLook()
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

    private void CheckPlayerBuilding()
    {
        if (IsBuildMode())
        {
            if (Common.GetControllerInputAxis(playerController.controllerID, "A Button") > 0.5f)
            {
                Debug.Log("A");
                World.Instance.buildingManager.BuildAt(GetPlayerSelection(), selectedBuilding);
            }
        }
    }

    void BuildingSelect()
    {
        
        if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Horizontal") < -0.5f) // Left
        {
            TowerText.color = Color.white;
            WallText.color = Color.red;
            ExtractorText.color = Color.white;
            ResearchText.color = Color.white;
            Debug.Log("Bleft");
            BuildingSelection = 1;
            
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Vertical") > 0.5f) // Up
        {
            TowerText.color = Color.red;
            WallText.color = Color.white;
            ExtractorText.color = Color.white;
            ResearchText.color = Color.white;
            Debug.Log("Bup");
            BuildingSelection = 2;
            


            //            selectedBuilding = towerBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Horizontal") > 0.5f) // Right
        {
            TowerText.color = Color.white;
            WallText.color = Color.white;
            ExtractorText.color = Color.red;
            ResearchText.color = Color.white;
            BuildingSelection = 3;
            Debug.Log("Bright");

            //            selectedBuilding = extractorBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Vertical") < -0.5f) // Down
        {
            TowerText.color = Color.white;
            WallText.color = Color.white;
            ExtractorText.color = Color.white;
            ResearchText.color = Color.red;
            BuildingSelection = 4;
            Debug.Log("Bdown");
            //            selectedBuilding = researchBuilding;
        }
        //else if (Common.GetControllerInputAxis(playerController.controllerID, "B Button") > 0.5f)
        //{
        //    TowerText.color = Color.white;
        //    WallText.color = Color.white;
        //    ExtractorText.color = Color.white;
        //    ResearchText.color = Color.white;
        //}
        if (BuildingSelection == 1)
        {
            if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f) // Left
            {
                WallChoice1.enabled = true;
                WallChoice2.enabled = true;
                ChoosingWall = true;
                ChoosingBuilding = false;
            }
        }
        else if (BuildingSelection == 2)
        {
            if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f)
            {
                TowerChoice1.enabled = true;
                TowerChoice2.enabled = true;
                TowerChoice3.enabled = true;
                ChoosingTower = true;
                ChoosingBuilding = false;
            }
        }
        else if (BuildingSelection == 3)
        {
            if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f)
            {
                selectedBuilding = extractorBuilding;
                CurrentSelectT.text = "E";
            }
        }
        else if (BuildingSelection == 4)
        {
            if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f)
            {
                selectedBuilding = researchBuilding;
                CurrentSelectT.text = "R";
            }
        }




    }

    void TowerSelect()
    {
        ChoosingBuilding = false;
       
        if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Horizontal") < -0.5f) // Left
        {
            TowerChoice1.color = Color.red;
            TowerChoice2.color = Color.white;
            TowerChoice3.color = Color.white;
            TowerSelection = 1;

            //selectedBuilding = wallBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Vertical") > 0.5f) // Up
        {
            TowerChoice1.color = Color.white;
            TowerChoice2.color = Color.red;
            TowerChoice3.color = Color.white;
            TowerSelection = 2;

            //            selectedBuilding = towerBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Horizontal") > 0.5f) // Right
        {
            TowerChoice1.color = Color.white;
            TowerChoice2.color = Color.white;
            TowerChoice3.color = Color.red;
            TowerSelection = 3;
            //            selectedBuilding = extractorBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "B Button") > 0.5f) // Down
        {
            TowerChoice1.color = Color.white;
            TowerChoice2.color = Color.white;
            TowerChoice3.color = Color.white;
            TowerChoice1.enabled = false;
            TowerChoice2.enabled = false;
            TowerChoice3.enabled = false;
            TowerSelection = 0;
            ChoosingBuilding = true;
            ChoosingTower = false;

            //            selectedBuilding = researchBuilding;
        }

        else if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f)
        {
            if (TowerSelection == 1)
            {
                TowerChoice1.color = Color.green;
                selectedBuilding = towerBuilding;
                CurrentSelectT.text = "T1";

            }
            else if (TowerSelection == 2)
            {
                TowerChoice2.color = Color.green;
                selectedBuilding = towerBuilding;
                CurrentSelectT.text = "T2";
            }
            else if (TowerSelection == 3)
            {
                TowerChoice3.color = Color.green;
                selectedBuilding = towerBuilding;
                CurrentSelectT.text = "T3";
            }
        }
    }
    void WallSelect()
    {
        ChoosingBuilding = false;
        
        if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Vertical") > 0.5f) // Up
        {
            WallChoice1.color = Color.red;
            WallChoice2.color = Color.white;
            WallSelection = 1;

            //            selectedBuilding = towerBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "B Button") > 0.5f)// Right
        {
            WallChoice1.color = Color.white;
            WallChoice2.color = Color.white;
            WallSelection = 0;
            WallChoice1.enabled = false;
            WallChoice2.enabled = false;
            ChoosingBuilding = true;
            ChoosingWall = false;
            //            selectedBuilding = extractorBuilding;
        }
        else if (Common.GetControllerInputAxis(playerController.controllerID, "D-Pad Vertical") < -0.5f) // Down
        {
            WallChoice1.color = Color.white;
            WallChoice2.color = Color.red;
            WallSelection = 2;


            //            selectedBuilding = researchBuilding;
        }

        if (Common.GetControllerInputAxis(playerController.controllerID, "X Button") > 0.5f)
        {
            if (WallSelection == 1)
            {
                WallChoice1.color = Color.green;
                selectedBuilding = wallBuilding;
                CurrentSelectT.text = "W1";
            }
            else if (WallSelection == 2)
            {
                WallChoice2.color = Color.green;
                selectedBuilding = wallBuilding;
                CurrentSelectT.text = "W2";
            }
        }
    }
}
