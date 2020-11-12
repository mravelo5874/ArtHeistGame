using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryScript
{
    // GLOBAL SCRIPT FOR INVENTORY -> use by simply calling 'InventoryScript.Variable' or 'InventoryScript.Function()'

    public static int money = 300;

    public static int difficultySetting = 0; // 0 is easy, 1 is medium, 2 is hard (expert)
    public static int[] difficultyThreshholds = { -100, 40, 80 };

    public static bool hasSpeedOnePills = false;
    public static GameObject speedPills = GameObject.Find("SpeedOnePills");

    public static bool hasHotCold = false;
    public static GameObject hotCold = GameObject.Find("HotCold");

    public static bool hasFilmCamera = false;
    public static bool hasDigitalCamera = false;
    public static bool hasZoomLens = false;
    public static bool hasNightVision = false;
    public static bool hasAccessCard = false;
    public static bool hasGuardKey = false;
    public static bool hasLargeCanvas = false;
    public static bool hasBlinker = false;
    public static bool hasDonut = false;

    public static void buySpeedOne()
    {
        if (hasSpeedOnePills) // already has them
        {
            Debug.Log("Already have Speed Pills!");
            return; // TODO: feedback to user that they already have them? (consider feedback to user for any number of problems that occur while shopping...)
        } else if(money < 100)
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
        money -= 100; // TODO: could make the cost of 'speedOnePills' into a variable listed at the top for easy access
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

        hotCold.GetComponent<Renderer>().material.SetColor("_Color",
            new Color(hotCold.GetComponent<Renderer>().material.color.r, hotCold.GetComponent<Renderer>().material.color.g, hotCold.GetComponent<Renderer>().material.color.b, 0.1f));
        hotCold.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");

        hasHotCold = true;
        money -= 100; // TODO: use constant instead of value
    }

    public static void buyFilmCamera()
    {
        if (hasFilmCamera) // already has them
        {
            return;
        }

        hasFilmCamera = true;
        money -= 100;
    }

    public static void buyDigitalCamera()
    {
        if (hasDigitalCamera) // already has them
        {
            return;
        }
        hasDigitalCamera = true;
        money -= 100;
    }

    public static void buyZoomLens()
    {
        if (hasZoomLens || (!hasFilmCamera && !hasDigitalCamera)) // already has them or does not own a camera
        {
            return;
        }
        hasZoomLens = true;
        money -= 100;
    }

    public static void buyNightVision()
    {
        if (hasNightVision || !hasGuardKey) // already has them or does not have guard keys yet
        {
            return;
        }
        hasNightVision = true;
        money -= 100;
    }

    public static void buyAccessCard()
    {
        if (hasAccessCard) // already has them
        {
            return;
        }
        hasAccessCard = true;
        money -= 100;
    }

    public static void buyGuardKey()
    {
        if (hasGuardKey) // already has them
        {
            return;
        }
        hasGuardKey = true;
        money -= 100;
    }

    public static void buyLargeCanvas()
    {
        if (hasLargeCanvas) // already has them
        {
            return;
        }
        hasLargeCanvas = true;
        money -= 100;
    }

    public static void buyBlinker()
    {
        if (hasBlinker) // already has them
        {
            return;
        }
        hasBlinker = true;
        money -= 100;
    }

    public static void buyDonut()
    {
        if (hasDonut) // already has them
        {
            return;
        }
        hasDonut = true;
        money -= 100;
    }
}
