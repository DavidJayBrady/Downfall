using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    //public declarations for menu variables

    // text boxes
    public Text ResolutionT;        
    public Text GraphicsT;
    public Text VolumeT;
    public Text FullscreenT;
    public Text BackT;
    public Text ResolutionSelectionT;
    public Text QualityT;

    //interactables
    public Slider VolumeS;
    public Toggle FullscreenTog;
    public Button BackB;

    //manage controller actions
    float Time = 0.25f;
    bool canInteract = true;
    int CurrentSelecting = 1;   //current controller selection

    int ResSelect = 0;      //scroll through resolution options
    int QualitySelect = 0;  //scroll through graphics options

    int[] GraphicsOptions = { 0, 1, 2, 3, 4, 5 };   // array for all graphics options
    public AudioMixer audioMixer;                   // do manage volume mixer
    Resolution[] resolutions;                       // gets all the resolutions available to computer

    private void Start()
    {
        // get object references;
        //text
        BackT = GameObject.Find("BackText").GetComponent<Text>();
        VolumeT = GameObject.Find("VolumeText").GetComponent<Text>();
        GraphicsT = GameObject.Find("GraphicsText").GetComponent<Text>();
        ResolutionT = GameObject.Find("ResolutionText").GetComponent<Text>();
        FullscreenT = GameObject.Find("FullscreenText").GetComponent<Text>();
        ResolutionSelectionT = GameObject.Find("ResolutionSelection").GetComponent<Text>();
        QualityT = GameObject.Find("QualityText").GetComponent<Text>();

        //interactables
        BackB = GameObject.Find("BackButton").GetComponent<Button>();
        VolumeS = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        FullscreenTog = GameObject.Find("FullscreenToggle").GetComponent<Toggle>();

        QualitySelect = QualitySettings.GetQualityLevel(); // get's current integer value for graphics
  
        // sets the quality initial quality level in the text box
        //it looks ugly but it's the best way i could think of
        if (QualitySelect == 0)
        {
            QualityT.text = "VERY LOW";
        }
        if (QualitySelect == 1)
        {
            QualityT.text = "LOW";
        }
        if (QualitySelect == 2)
        {
            QualityT.text = "MEDIUM";
        }
        if (QualitySelect == 3)
        {
            QualityT.text = "HIGH";
        }
        if (QualitySelect == 4)
        {
            QualityT.text = "VERY HIGH";
        }
        if (QualitySelect == 5)
        {
            QualityT.text = "ULTRA";
        }

        resolutions = Screen.resolutions;   // finds default screen resolution
        ResolutionSelectionT.text = Screen.width.ToString() + " x " + Screen.height.ToString(); // sets the text box to display current resolution


        CurrentSelecting = 1;   //initialize current selection
    }

    // to slow down the controller
    IEnumerator UpdateGUI(float updateTime)
    {

        if (CurrentSelecting == 6)  // we dont have 6 options
        {
            CurrentSelecting = 1;
        }
        else if (CurrentSelecting == 0) // we also dont have a 0 options
        {
            CurrentSelecting = 5;
        }

        //displays current selection
        if (CurrentSelecting == 1)
        {
            BackT.color = Color.red;
            VolumeT.color = Color.white;
            GraphicsT.color = Color.white;
            ResolutionT.color = Color.white;
            FullscreenT.color = Color.white;
        }
        else if (CurrentSelecting == 2)
        {
            BackT.color = Color.white;
            VolumeT.color = Color.red;
            GraphicsT.color = Color.white;
            ResolutionT.color = Color.white;
            FullscreenT.color = Color.white;
        }
        else if (CurrentSelecting == 3)
        {
            BackT.color = Color.white;
            VolumeT.color = Color.white;
            GraphicsT.color = Color.red;
            ResolutionT.color = Color.white;
            FullscreenT.color = Color.white;
        }
        else if (CurrentSelecting == 4)
        {
            BackT.color = Color.white;
            VolumeT.color = Color.white;
            GraphicsT.color = Color.white;
            ResolutionT.color = Color.red;
            FullscreenT.color = Color.white;
        }
        else if (CurrentSelecting == 5)
        {
            BackT.color = Color.white;
            VolumeT.color = Color.white;
            GraphicsT.color = Color.white;
            ResolutionT.color = Color.white;
            FullscreenT.color = Color.red;
        }
        ////////////////////////////////

        
        yield return new WaitForSeconds(updateTime);    //slow down
        canInteract = true; //declares when when the controller can move again
        //Debug.Log("Gui updated");
    }

    void Update()
    {

//        Mainmenu.SetActive(false);      //hides the menu, 
                                        //this is in case the player goes back and forth 
                                        //between menu and the void start isnt reached

        float Scroll = Input.GetAxis("Vertical");   //gets user input for joystick

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
        if (Input.GetButtonUp("AButton"))
        {
            //back button
            if (CurrentSelecting == 1)
            {

                SceneManager.LoadScene("MainMenu");
            }
            //volume
            else if (CurrentSelecting == 2)
            {
                VolumeS.Select();   //volume mixer
            }
            //graphics
            else if (CurrentSelecting == 3)
            {
                SetQuality();       //switch graphics
            }
            //resolution
            else if (CurrentSelecting == 4)
            {
                SetResolution();    //switch resolution
            }
            //fullscreen
            else if (CurrentSelecting == 5)
            {
                FullscreenTog.Select(); //toggle fullscreen
            }
        }
    }

    //resolution
    public void SetResolution ()
    {

        ResSelect++;    //increments current resolution
        if (ResSelect >= resolutions.Length)    //if we exceed the list
        {
            ResSelect = 0;  //start over
        }

        Resolution resolution = resolutions[ResSelect]; // get resolution

        ResolutionSelectionT.text = resolution.width.ToString() + " x " + resolution.height.ToString(); // display resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);   //set resoluition
    }

    //volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);  //toggle volume
    }

    // set graphics
    public void SetQuality ()
    {
        QualitySelect++;    //increment selection
        if (QualitySelect >= GraphicsOptions.Length)    //if we exceed list
        {
            QualitySelect = 0;  //start over
        }

        QualitySettings.SetQualityLevel(QualitySelect); //set quality

        //displays quality
        if (QualitySelect == 0)
        {
            QualityT.text = "VERY LOW";
        }
        if (QualitySelect == 1)
        {
            QualityT.text = "LOW";
        }
        if (QualitySelect == 2)
        {
            QualityT.text = "MEDIUM";
        }
        if (QualitySelect == 3)
        {
            QualityT.text = "HIGH";
        }
        if (QualitySelect == 4)
        {
            QualityT.text = "VERY HIGH";
        }
        if (QualitySelect == 5)
        {
            QualityT.text = "ULTRA";
        }
    }

    //fullscreen
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //turn on or off
    }
}
