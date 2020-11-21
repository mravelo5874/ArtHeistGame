using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDigitalCamera : MonoBehaviour
{
    public static string name = "Digital Camera";
    public static string info = "This allows you to take pictures.";
    public static Color startColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;

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
    }

    private void OnMouseExit()
    {
        ShopItemInfo.ChangeItemInfo("", "");
    }
}