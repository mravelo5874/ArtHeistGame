using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDigitalCamera : MonoBehaviour
{
    public static string name = "Digital Camera";
    public static string info = "Allows you to bring up a camera and take pictures for easier painting recreation.";
    public static Color startColor;
    public static GameObject outline;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;
        outline = GameObject.Find("DigitalCameraOutline");
        outline.GetComponent<Renderer>().enabled = false;

        if (InventoryScript.hasDigitalCamera)
        {
            // make it look like it was bought
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, 0.1f));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
    }

    private void OnMouseDown()
    {
        InventoryScript.buyDigitalCamera();
    }

    private void OnMouseEnter()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
        if(!InventoryScript.hasDigitalCamera)
        {
            outline.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        ShopItemInfo.ChangeItemInfo("", "");
        outline.GetComponent<Renderer>().enabled = false;
    }
}