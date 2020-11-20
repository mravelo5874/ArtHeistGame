using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroHelpScript : MonoBehaviour
{

    public TextMeshProUGUI helperText;

    public static int currentTask = 0;

    // Start is called before the first frame update
    void Start()
    {
        // do nothing at the start?
        // figure out if it should be displayed?
    }

    // Update is called once per frame
    void Update()
    {
        // moving forward = next task
        if (Input.GetKey(KeyCode.W) && currentTask == 0)
        {
            currentTask = 1;
        }

        if (Input.GetKey(KeyCode.X) && currentTask == 1)
        {
            currentTask = 2;
        }

        if (Input.GetKey(KeyCode.LeftShift) && currentTask == 2)
        {
            currentTask = 3;
        }

        if (Input.GetKey(KeyCode.LeftControl) && currentTask == 3)
        {
            currentTask = 4;
        }

        if (Input.GetKey(KeyCode.R) && currentTask == 4)
        {
            currentTask = 5;
        }

        if (Input.GetKey(KeyCode.R) && currentTask == 5)
        {
            currentTask = 6; // need to unpause, so hit R again
        }

        if (Input.GetKey(KeyCode.Tab) && currentTask == 6)
        {
            currentTask = 7;
        }

        // currentTask = 8; // handled by ObjectivesMenuScript -> CheckCompleteObjective()

        if (Input.GetKey(KeyCode.F) && currentTask == 8)
        {
            currentTask = 9;
        }

        if (GameHelper.GetCurrentLevel().levelName == "Level 1")
        {
            if (currentTask == 0)
            {
                helperText.text = "WASD to Move";
            }
            else if (currentTask == 1)
            {
                helperText.text = "X to switch POV";
            }
            else if (currentTask == 2)
            {
                helperText.text = "LeftShift to Run";
            }
            else if (currentTask == 3)
            {
                helperText.text = "LeftControl to Crouch";
            }
            else if (currentTask == 4)
            {
                helperText.text = "R to Pause";
            }
            else if (currentTask == 5)
            {
                helperText.text = ""; // don't interfere with pause menu text 
            }
            else if (currentTask == 6)
            {
                helperText.text = "Tab for Objectives";
            }
            else if (currentTask == 7)
            {
                helperText.text = "Look at paintings - click and hold to capture";
            }
            else if (currentTask == 8)
            {
                helperText.text = "Find all objectives, then exit on the green circle.";
            }
        } else
        {
            helperText.text = "";
        }

        if (GameHelper.GetCurrentLevel().levelName == "Level 4" && InventoryScript.hasDigitalCamera)
        {
            if (currentTask == 8)
            {
                helperText.text = "Press F to toggle the camera";
            }
            else if (currentTask == 9)
            {
                helperText.text = "";
            }
        }
    }
}
