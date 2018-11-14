using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScr : MonoBehaviour
{
    public Image AttackerControls;
    public Image DefenderControls;



    //manage controller actions
    float Time = 0.25f;
    bool canInteract = true;
    int CurrentSelecting = 1;   //current controller selection


    // Start is called before the first frame update
    void Start()
    {
        // menus

        AttackerControls = GameObject.Find("AttackerControls").GetComponent<Image>();
        DefenderControls = GameObject.Find("DefenderControls").GetComponent<Image>();

        CurrentSelecting = 1;   //initialize current selection

    }

    IEnumerator UpdateGUI(float updateTime)
    {

        if (CurrentSelecting == 3)  // we dont have 2 options
        {
            CurrentSelecting = 1;
        }
        else if (CurrentSelecting == 0) // we also dont have a 0 options
        {
            CurrentSelecting = 2;
        }

        if (CurrentSelecting == 1)
        {
            AttackerControls.enabled = true;
            DefenderControls.enabled = false;

        }
        else if (CurrentSelecting == 2)
        {
            DefenderControls.enabled = true;
            AttackerControls.enabled = false;

        }

        yield return new WaitForSeconds(updateTime);    //slow down
        canInteract = true; //declares when when the controller can move again
    }

        // Update is called once per frame
        void Update()
    {

        float Scroll = Common.GetAllInputAxis("Left Horizontal");   //gets user input for joystick

        if (Scroll == -1 && canInteract)
        {
            //  Debug.Log("Go up");
            CurrentSelecting += -1; //scrolls
            canInteract = false;       //pause
            StartCoroutine(UpdateGUI(Time)); //start coroutine
        }
        else if (Scroll == 1 && canInteract)
        {
            //     Debug.Log("Go Down");
            CurrentSelecting += 1;
            canInteract = false;
            StartCoroutine(UpdateGUI(Time));
        }

        //get's user button input
        if (Common.GetAllInputAxis("A Button") == 1)
        {

            SceneManager.LoadScene("MainMenu");
        }
    }
}
