using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryScript
{
    // GLOBAL SCRIPT FOR INVENTORY -> use by simply calling 'InventoryScript.Variable' or 'InventoryScript.Function()'

    public static int money = 3000;

    public static int difficultySetting = 0; // 0 is easy, 1 is medium, 2 is hard (expert)
    public static int[] difficultyThreshholds = { -100, 40, 80 };

    public static bool hasSpeedOnePills = false;
    public static GameObject speedPills = GameObject.Find("SpeedOnePills");

    public static bool hasHotCold = false;
    public static GameObject hotCold = GameObject.Find("HotCold");

    public static bool hasDigitalCamera = false;
    public static GameObject digitalCamera = GameObject.Find("DigitalCamera");

    public static void buySpeedOne()
    {
        if (hasSpeedOnePills) // already has them
        {
            Debug.Log("Already have Speed Pills!");
            return; // TODO: feedback to user that they already have them? (consider feedback to user for any number of problems that occur while shopping...)
        } else if(money < 200)
        {
            Debug.Log("Not enough money for Speed Pills!");
            return;
        }

        // doing it again cause maybe didn't exist yet...
        speedPills = GameObject.Find("SpeedOnePills");
        speedPills.GetComponent<Renderer>().material.SetColor("_Color",
            new Color(speedPills.GetComponent<Renderer>().material.color.r, speedPills.GetComponent<Renderer>().material.color.g, speedPills.GetComponent<Renderer>().material.color.b, 0.1f));
        speedPills.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");

        hasSpeedOnePills = true;
        money -= 200; // TODO: could make the cost of 'speedOnePills' into a variable listed at the top for easy access
    }

    public static void buyHotCold()
    {
        if (hasHotCold)
        {
            Debug.Log("Already have Hot-or-Cold Pills!");
            return; // feedback to user if any check fails...
        }
        else if (money < 100)
        {
            Debug.Log("Not enough money for Hot-or-Cold Pills!");
            return;
        }

        hotCold = GameObject.Find("HotCold");

        hotCold.GetComponent<Renderer>().material.SetColor("_Color",
            new Color(hotCold.GetComponent<Renderer>().material.color.r, hotCold.GetComponent<Renderer>().material.color.g, hotCold.GetComponent<Renderer>().material.color.b, 0.1f));
        hotCold.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");

        hasHotCold = true;
        money -= 100; // TODO: use constant instead of value
    }

    public static void buyDigitalCamera()
    {
        if (hasDigitalCamera) // already has them
        {
            Debug.Log("Already have the Camera!");
            return;
        }
        else if (money < 300)
        {
            Debug.Log("Not enough money for the Camera!");
            return;
        }

        digitalCamera = GameObject.Find("DigitalCamera");

        digitalCamera.GetComponent<Renderer>().material.SetColor("_Color",
            new Color(digitalCamera.GetComponent<Renderer>().material.color.r, digitalCamera.GetComponent<Renderer>().material.color.g, digitalCamera.GetComponent<Renderer>().material.color.b, 0.1f));
        digitalCamera.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");

        hasDigitalCamera = true;
        money -= 300;
    }


}
