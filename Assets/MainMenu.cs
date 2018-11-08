using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //declarations

    //menus
    //public GameObject Mainmenu;
    //public GameObject Optionsmenu;
    //public GameObject ControllerMenu;
    //text
    public Text PlayT;
    public Text OptionsT;
    public Text QuitT;
    public Text ControlT;

    public Button GOptions;

    //to slow down controller
    float Time = 0.25f;
    bool canInteract = true;

    int CurrentSelecting = 1;           //current selection

    void Start()
    {
        //references

        //menu
        //Mainmenu = GameObject.Find("MainMenu");
        //Optionsmenu = GameObject.Find("OptionsMenu");
        //ControllerMenu = GameObject.Find("ControlScheme");

        //text
        PlayT = GameObject.Find("PlayText").GetComponent<Text>();
        OptionsT = GameObject.Find("OptionsText").GetComponent<Text>();
        QuitT = GameObject.Find("QuitText").GetComponent<Text>();
        ControlT = GameObject.Find("ControlsText").GetComponent<Text>();

        //button
        GOptions = GameObject.Find("Options").GetComponent<Button>();
        

        //Optionsmenu.SetActive(true);    //enable
        //Optionsmenu.SetActive(false);   //hide
        //ControllerMenu.SetActive(true);    //enable
        //ControllerMenu.SetActive(false);   //hide
    }

    //to slow down controller
    IEnumerator UpdateGUI(float updateTime)
    {
        //scroll through options

        //we dont have 4 options
        if (CurrentSelecting == 5)
        {
            CurrentSelecting = 1;
        }
        //we dont have 0 options
        else if (CurrentSelecting == 0)
        {
            CurrentSelecting = 4;
        }

        //possible selections
        if (CurrentSelecting == 1)
        {
            PlayT.color = Color.red;
            ControlT.color = Color.white;
            OptionsT.color = Color.white;
            QuitT.color = Color.white;
         }
        else if (CurrentSelecting == 2)
        {
            PlayT.color = Color.white;
            ControlT.color = Color.red;
            OptionsT.color = Color.white;
            QuitT.color = Color.white;
        }
        else if (CurrentSelecting == 3)
        {
            PlayT.color = Color.white;
            ControlT.color = Color.white;
            OptionsT.color = Color.red;
            QuitT.color = Color.white;
        }
        else if (CurrentSelecting == 4)
        {
            PlayT.color = Color.white;
            ControlT.color = Color.white;
            OptionsT.color = Color.white;
            QuitT.color = Color.red;
        }

        yield return new WaitForSeconds(updateTime);    //slow down
        canInteract = true;
//        Debug.Log("Gui updated");
    }

   

    void Update()
    {
        
        float Scroll = Input.GetAxis("Vertical");   // get joystick input

        //mover around depending on joystick
        if (Scroll == 1 && canInteract)
        {
          //  Debug.Log("Go up");
            CurrentSelecting += -1; //move
            canInteract = false;       //pause
            StartCoroutine(UpdateGUI(Time));    //start coroutine
        }
        else if (Scroll == -1 && canInteract)
        {
       //     Debug.Log("Go Down");
            CurrentSelecting += 1;
            canInteract = false;
            StartCoroutine(UpdateGUI(Time));
        }


        //input for A button
        if (Input.GetButtonUp("AButton"))
        {
            if (CurrentSelecting == 1)
            {
                SceneManager.LoadScene("AbuScene"); //   PlayGame();     //play game
            }
            else if (CurrentSelecting ==2)
            {
                SceneManager.LoadScene("ControllerMenu"); // ptionsmenu.SetActive(true);   // activate options menu 
            }
            else if (CurrentSelecting == 3)
            {
                SceneManager.LoadScene("OptionsMenu");   // ControllerMenu.SetActive(true);   // activate options menu 
            }
            else if (CurrentSelecting ==4)
            {
                QuitGame(); //quit
            }
        }
    }




    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit(); // guits game
    }
}
