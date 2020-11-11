using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroHelpScript : MonoBehaviour
{

    public TextMeshProUGUI helperText;

    int currentTask = 0;

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

        if (Input.GetKey(KeyCode.R) && currentTask == 2)
        {
            currentTask = 3;
        }

        if (Input.GetKey(KeyCode.Tab) && currentTask == 3)
        {
            currentTask = 4;
        }

        if (Input.GetMouseButtonDown(0) && currentTask == 4)
        {
            currentTask = 5;
        }

        if (GameHelper.GetCurrentLevel().levelName == "Level 1")
        {
            if (currentTask == 0)
            {
                helperText.text = "WASD to Move";
            } else if (currentTask == 1)
            {
                helperText.text = "X to switch between POVs";
            } else if (currentTask == 2)
            {
                helperText.text = "R to open Pause Menu";
            } else if (currentTask == 3)
            {
                helperText.text = "TAB to open Objectives";
            } else if (currentTask == 4)
            {
                helperText.text = "Hold Left Mouse When \nLooking at a painting to \nadd it";
            } else if (currentTask == 5)
            {
                helperText.text = "";
            }
        } else
        {
            helperText.text = "";
        }
    }
}
