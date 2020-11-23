using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpeedOne : MonoBehaviour
{
    public static string name = "\"Speed\" Pills";
    public static string info = "These unidentified pharmaceuticals will allow you to run faster.";
    public static Color startColor;
    public static GameObject outline;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;
        outline = GameObject.Find("SpeedOnePillsOutline");
        outline.GetComponent<Renderer>().enabled = false;

        if (InventoryScript.hasSpeedOnePills)
        {
            // make it look like it was bought
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, 0.1f));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
    }

    private void OnMouseDown()
    {
        InventoryScript.buySpeedOne();
    }

    private void OnMouseEnter()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
        if (!InventoryScript.hasSpeedOnePills)
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
